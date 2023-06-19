﻿Feature: OAuthConfiguration
	Get the OAUTHConfiguration and check its content

Scenario: Get the configuration
	When execute HTTP GET request 'https://localhost:8080/.well-known/oauth-authorization-server'
	| Key | Value |
	And extract JSON from body

	Then HTTP status code equals to '200'
	And JSON 'issuer'='https://localhost:8080'
	And JSON 'authorization_endpoint'='https://localhost:8080/authorization'
	And JSON 'registration_endpoint'='https://localhost:8080/register'
	And JSON 'token_endpoint'='https://localhost:8080/token'
	And JSON 'revocation_endpoint'='https://localhost:8080/token/revoke'
	And JSON 'jwks_uri'='https://localhost:8080/jwks'
	And JSON 'tls_client_certificate_bound_access_tokens'='true'
	And JSON '$.response_types_supported[0]'='code'
	And JSON '$.response_types_supported[1]'='code id_token'
	And JSON '$.response_types_supported[2]'='code id_token token'
	And JSON '$.response_types_supported[3]'='code token'
	And JSON '$.response_types_supported[4]'='id_token'
	And JSON '$.response_types_supported[5]'='id_token token'
	And JSON '$.response_modes_supported[0]'='query'
	And JSON '$.response_modes_supported[1]'='fragment'
	And JSON '$.response_modes_supported[2]'='form_post'
	And JSON '$.grant_types_supported[0]'='client_credentials'
	And JSON '$.grant_types_supported[1]'='refresh_token'
	And JSON '$.grant_types_supported[2]'='password'
	And JSON '$.grant_types_supported[3]'='urn:openid:params:grant-type:ciba'
	And JSON '$.grant_types_supported[4]'='urn:ietf:params:oauth:grant-type:uma-ticket'
	And JSON '$.grant_types_supported[5]'='urn:ietf:params:oauth:grant-type:device_code'
	And JSON '$.grant_types_supported[6]'='urn:ietf:params:oauth:grant-type:pre-authorized_code'
	And JSON '$.grant_types_supported[7]'='authorization_code'
	And JSON '$.grant_types_supported[8]'='implicit'
	And JSON '$.token_endpoint_auth_methods_supported[0]'='private_key_jwt'
	And JSON '$.token_endpoint_auth_methods_supported[1]'='client_secret_basic'
	And JSON '$.token_endpoint_auth_methods_supported[2]'='client_secret_jwt'
	And JSON '$.token_endpoint_auth_methods_supported[3]'='client_secret_post'
	And JSON '$.token_endpoint_auth_methods_supported[4]'='pkce'
	And JSON '$.token_endpoint_auth_methods_supported[5]'='tls_client_auth'
	And JSON '$.token_endpoint_auth_methods_supported[6]'='self_signed_tls_client_auth'
	And JSON '$.token_endpoint_auth_signing_alg_values_supported[0]'='RS256'