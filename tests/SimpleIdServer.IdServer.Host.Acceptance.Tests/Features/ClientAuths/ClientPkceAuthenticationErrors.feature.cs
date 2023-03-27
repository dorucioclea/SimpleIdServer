﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace SimpleIdServer.IdServer.Host.Acceptance.Tests.Features.ClientAuths
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class ClientPkceAuthenticationErrorsFeature : object, Xunit.IClassFixture<ClientPkceAuthenticationErrorsFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "ClientPkceAuthenticationErrors.feature"
#line hidden
        
        public ClientPkceAuthenticationErrorsFeature(ClientPkceAuthenticationErrorsFeature.FixtureData fixtureData, SimpleIdServer_IdServer_Host_Acceptance_Tests_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features/ClientAuths", "ClientPkceAuthenticationErrors", "\tCheck errors returned during the \'pkce\' authentication", ProgrammingLanguage.CSharp, featureTags);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public void TestInitialize()
        {
        }
        
        public void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Error is returned when code_verifier is missing")]
        [Xunit.TraitAttribute("FeatureTitle", "ClientPkceAuthenticationErrors")]
        [Xunit.TraitAttribute("Description", "Error is returned when code_verifier is missing")]
        public void ErrorIsReturnedWhenCode_VerifierIsMissing()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Error is returned when code_verifier is missing", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 4
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table86 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table86.AddRow(new string[] {
                            "grant_type",
                            "client_credentials"});
                table86.AddRow(new string[] {
                            "scope",
                            "scope"});
                table86.AddRow(new string[] {
                            "client_id",
                            "nineClient"});
#line 5
 testRunner.When("execute HTTP POST request \'https://localhost:8080/token\'", ((string)(null)), table86, "When ");
#line hidden
#line 11
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 12
 testRunner.Then("HTTP status code equals to \'400\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 13
 testRunner.And("JSON \'$.error\'=\'invalid_client\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 14
 testRunner.And("JSON \'$.error_description\'=\'missing parameter code_verifier\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Error is returned when code is missing")]
        [Xunit.TraitAttribute("FeatureTitle", "ClientPkceAuthenticationErrors")]
        [Xunit.TraitAttribute("Description", "Error is returned when code is missing")]
        public void ErrorIsReturnedWhenCodeIsMissing()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Error is returned when code is missing", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 16
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table87 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table87.AddRow(new string[] {
                            "grant_type",
                            "client_credentials"});
                table87.AddRow(new string[] {
                            "scope",
                            "scope"});
                table87.AddRow(new string[] {
                            "client_id",
                            "nineClient"});
                table87.AddRow(new string[] {
                            "code_verifier",
                            "code"});
#line 17
 testRunner.When("execute HTTP POST request \'https://localhost:8080/token\'", ((string)(null)), table87, "When ");
#line hidden
#line 24
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 25
 testRunner.Then("HTTP status code equals to \'400\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 26
 testRunner.And("JSON \'$.error\'=\'invalid_client\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 27
 testRunner.And("JSON \'$.error_description\'=\'bad client credential\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Error is returned when code doesn\'t exist")]
        [Xunit.TraitAttribute("FeatureTitle", "ClientPkceAuthenticationErrors")]
        [Xunit.TraitAttribute("Description", "Error is returned when code doesn\'t exist")]
        public void ErrorIsReturnedWhenCodeDoesntExist()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Error is returned when code doesn\'t exist", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 29
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table88 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table88.AddRow(new string[] {
                            "grant_type",
                            "client_credentials"});
                table88.AddRow(new string[] {
                            "scope",
                            "scope"});
                table88.AddRow(new string[] {
                            "client_id",
                            "nineClient"});
                table88.AddRow(new string[] {
                            "code_verifier",
                            "code"});
                table88.AddRow(new string[] {
                            "code",
                            "code"});
#line 30
 testRunner.When("execute HTTP POST request \'https://localhost:8080/token\'", ((string)(null)), table88, "When ");
#line hidden
#line 38
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 39
 testRunner.Then("HTTP status code equals to \'400\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 40
 testRunner.And("JSON \'$.error\'=\'invalid_client\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 41
 testRunner.And("JSON \'$.error_description\'=\'bad client credential\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Error is returned when code_verifier is invalid")]
        [Xunit.TraitAttribute("FeatureTitle", "ClientPkceAuthenticationErrors")]
        [Xunit.TraitAttribute("Description", "Error is returned when code_verifier is invalid")]
        public void ErrorIsReturnedWhenCode_VerifierIsInvalid()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Error is returned when code_verifier is invalid", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 43
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 44
 testRunner.Given("authenticate a user", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
                TechTalk.SpecFlow.Table table89 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table89.AddRow(new string[] {
                            "response_type",
                            "code"});
                table89.AddRow(new string[] {
                            "client_id",
                            "nineClient"});
                table89.AddRow(new string[] {
                            "state",
                            "state"});
                table89.AddRow(new string[] {
                            "redirect_uri",
                            "http://localhost:8080"});
                table89.AddRow(new string[] {
                            "response_mode",
                            "query"});
                table89.AddRow(new string[] {
                            "scope",
                            "secondScope"});
                table89.AddRow(new string[] {
                            "code_challenge_method",
                            "plain"});
                table89.AddRow(new string[] {
                            "code_challenge",
                            "challenge"});
#line 45
 testRunner.When("execute HTTP GET request \'https://localhost:8080/authorization\'", ((string)(null)), table89, "When ");
#line hidden
#line 56
 testRunner.And("extract parameter \'code\' from redirect url", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table90 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table90.AddRow(new string[] {
                            "grant_type",
                            "client_credentials"});
                table90.AddRow(new string[] {
                            "scope",
                            "scope"});
                table90.AddRow(new string[] {
                            "client_id",
                            "nineClient"});
                table90.AddRow(new string[] {
                            "code_verifier",
                            "invalid"});
                table90.AddRow(new string[] {
                            "code",
                            "$code$"});
#line 58
 testRunner.And("execute HTTP POST request \'https://localhost:8080/token\'", ((string)(null)), table90, "And ");
#line hidden
#line 66
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 67
 testRunner.Then("HTTP status code equals to \'400\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 68
 testRunner.And("JSON \'$.error\'=\'invalid_client\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 69
 testRunner.And("JSON \'$.error_description\'=\'code_verifier is invalid\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                ClientPkceAuthenticationErrorsFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                ClientPkceAuthenticationErrorsFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
