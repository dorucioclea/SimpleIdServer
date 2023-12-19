﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.Math;
using System;
using System.IO;
using System.Linq;

namespace SimpleIdServer.Did.Crypto;

public class DeterministicECDSA : ECDsaSigner
{
    private readonly IDigest _digest;
    private byte[] _buffer = new byte[0];

    public DeterministicECDSA() : base(new HMacDsaKCalculator(new Sha256Digest()))

    {
        _digest = new Sha256Digest();
    }

    public DeterministicECDSA(Func<IDigest> digest)
        : base(new HMacDsaKCalculator(digest()))
    {
        _digest = digest();
    }


    public void SetPrivateKey(ECPrivateKeyParameters ecKey)
    {
        Init(true, ecKey);
    }

    public byte[] Sign()
    {
        var hash = new byte[_digest.GetDigestSize()];
        _digest.BlockUpdate(_buffer, 0, _buffer.Length);
        _digest.DoFinal(hash, 0);
        _digest.Reset();
        return SignHash(hash);
    }

    public byte[] SignHash(byte[] hash)
    {
        return new ECDSASignature(GenerateSignature(hash)).ToDER();
    }

    public void Update(byte[] buf)
    {
        _buffer = _buffer.Concat(buf).ToArray();
    }
}

public class ECDSASignature
{
    public ECDSASignature(BigInteger r, BigInteger s)
    {
        R = r;
        S = s;
    }

    public ECDSASignature(BigInteger[] rs)
    {
        R = rs[0];
        S = rs[1];
    }

    public ECDSASignature(byte[] derSig)
    {
        var decoder = new Asn1InputStream(derSig);
        var seq = decoder.ReadObject() as DerSequence;
        R = ((DerInteger)seq[0]).Value;
        S = ((DerInteger)seq[1]).Value;
    }

    public BigInteger R { get; }
    public BigInteger S { get; }
    public byte[] V { get; set; }

    public static ECDSASignature FromDER(byte[] sig)
    {
        return new ECDSASignature(sig);
    }

    public byte[] ToDER()
    {
        var bos = new MemoryStream(72);
        var seq = new DerSequenceGenerator(bos);
        seq.AddObject(new DerInteger(R));
        seq.AddObject(new DerInteger(S));
        seq.Close();
        return bos.ToArray();
    }
}