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
namespace SimpleIdServer.IdServer.Host.Acceptance.Tests.Features
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class GrantErrorsFeature : object, Xunit.IClassFixture<GrantErrorsFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "GrantErrors.feature"
#line hidden
        
        public GrantErrorsFeature(GrantErrorsFeature.FixtureData fixtureData, SimpleIdServer_IdServer_Host_Acceptance_Tests_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features", "GrantErrors", "\tCheck errors returned by grants API", ProgrammingLanguage.CSharp, featureTags);
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
        
        [Xunit.SkippableFactAttribute(DisplayName="access token is required")]
        [Xunit.TraitAttribute("FeatureTitle", "GrantErrors")]
        [Xunit.TraitAttribute("Description", "access token is required")]
        public void AccessTokenIsRequired()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("access token is required", null, tagsOfScenario, argumentsOfScenario, featureTags);
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
                TechTalk.SpecFlow.Table table119 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
#line 5
 testRunner.When("execute HTTP GET request \'http://localhost/grants/id\'", ((string)(null)), table119, "When ");
#line hidden
#line 8
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 10
 testRunner.Then("JSON \'error\'=\'access_denied\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 11
 testRunner.And("JSON \'error_description\'=\'missing token\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="grant must exists")]
        [Xunit.TraitAttribute("FeatureTitle", "GrantErrors")]
        [Xunit.TraitAttribute("Description", "grant must exists")]
        public void GrantMustExists()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("grant must exists", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 13
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table120 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table120.AddRow(new string[] {
                            "Authorization",
                            "Bearer AT"});
#line 14
 testRunner.When("execute HTTP GET request \'http://localhost/grants/id\'", ((string)(null)), table120, "When ");
#line hidden
#line 18
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 20
 testRunner.Then("HTTP status code equals to \'404\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 21
 testRunner.And("JSON \'error\'=\'invalid_target\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 22
 testRunner.And("JSON \'error_description\'=\'the grant id doesn\'t exist\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="access token must be valid")]
        [Xunit.TraitAttribute("FeatureTitle", "GrantErrors")]
        [Xunit.TraitAttribute("Description", "access token must be valid")]
        public void AccessTokenMustBeValid()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("access token must be valid", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 24
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 25
 testRunner.Given("authenticate a user", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
                TechTalk.SpecFlow.Table table121 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table121.AddRow(new string[] {
                            "response_type",
                            "code"});
                table121.AddRow(new string[] {
                            "client_id",
                            "fortySevenClient"});
                table121.AddRow(new string[] {
                            "state",
                            "state"});
                table121.AddRow(new string[] {
                            "response_mode",
                            "query"});
                table121.AddRow(new string[] {
                            "redirect_uri",
                            "http://localhost:8080"});
                table121.AddRow(new string[] {
                            "nonce",
                            "nonce"});
                table121.AddRow(new string[] {
                            "claims",
                            "{ \"id_token\": { \"acr\": { \"essential\" : true, \"value\": \"urn:openbanking:psd2:ca\" }" +
                                " } }"});
                table121.AddRow(new string[] {
                            "resource",
                            "https://cal.example.com"});
                table121.AddRow(new string[] {
                            "grant_management_action",
                            "create"});
                table121.AddRow(new string[] {
                            "scope",
                            "grant_management_query"});
#line 27
 testRunner.When("execute HTTP GET request \'http://localhost/authorization\'", ((string)(null)), table121, "When ");
#line hidden
#line 40
 testRunner.And("extract parameter \'code\' from redirect url", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table122 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table122.AddRow(new string[] {
                            "client_id",
                            "fortySevenClient"});
                table122.AddRow(new string[] {
                            "client_secret",
                            "password"});
                table122.AddRow(new string[] {
                            "grant_type",
                            "authorization_code"});
                table122.AddRow(new string[] {
                            "code",
                            "$code$"});
                table122.AddRow(new string[] {
                            "redirect_uri",
                            "http://localhost:8080"});
#line 42
 testRunner.And("execute HTTP POST request \'https://localhost:8080/token\'", ((string)(null)), table122, "And ");
#line hidden
#line 50
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 51
 testRunner.And("extract parameter \'$.grant_id\' from JSON body into \'grantId\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table123 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table123.AddRow(new string[] {
                            "Authorization",
                            "Bearer INVALID"});
#line 53
 testRunner.And("execute HTTP GET request \'http://localhost/grants/$grantId$\'", ((string)(null)), table123, "And ");
#line hidden
#line 57
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 59
 testRunner.Then("HTTP status code equals to \'401\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 60
 testRunner.And("JSON \'error\'=\'invalid_token\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 61
 testRunner.And("JSON \'error_description\'=\'either the access token has been revoked or is invalid\'" +
                        "", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="only the same client can query the grant")]
        [Xunit.TraitAttribute("FeatureTitle", "GrantErrors")]
        [Xunit.TraitAttribute("Description", "only the same client can query the grant")]
        public void OnlyTheSameClientCanQueryTheGrant()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("only the same client can query the grant", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 63
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 64
 testRunner.Given("authenticate a user", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
                TechTalk.SpecFlow.Table table124 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table124.AddRow(new string[] {
                            "response_type",
                            "code"});
                table124.AddRow(new string[] {
                            "client_id",
                            "fortySevenClient"});
                table124.AddRow(new string[] {
                            "state",
                            "state"});
                table124.AddRow(new string[] {
                            "response_mode",
                            "query"});
                table124.AddRow(new string[] {
                            "redirect_uri",
                            "http://localhost:8080"});
                table124.AddRow(new string[] {
                            "nonce",
                            "nonce"});
                table124.AddRow(new string[] {
                            "claims",
                            "{ \"id_token\": { \"acr\": { \"essential\" : true, \"value\": \"urn:openbanking:psd2:ca\" }" +
                                " } }"});
                table124.AddRow(new string[] {
                            "resource",
                            "https://cal.example.com"});
                table124.AddRow(new string[] {
                            "grant_management_action",
                            "create"});
                table124.AddRow(new string[] {
                            "scope",
                            "grant_management_revoke"});
#line 66
 testRunner.When("execute HTTP GET request \'http://localhost/authorization\'", ((string)(null)), table124, "When ");
#line hidden
#line 79
 testRunner.And("extract parameter \'code\' from redirect url", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table125 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table125.AddRow(new string[] {
                            "client_id",
                            "fortySevenClient"});
                table125.AddRow(new string[] {
                            "client_secret",
                            "password"});
                table125.AddRow(new string[] {
                            "grant_type",
                            "authorization_code"});
                table125.AddRow(new string[] {
                            "code",
                            "$code$"});
                table125.AddRow(new string[] {
                            "redirect_uri",
                            "http://localhost:8080"});
#line 81
 testRunner.And("execute HTTP POST request \'https://localhost:8080/token\'", ((string)(null)), table125, "And ");
#line hidden
#line 89
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 90
 testRunner.And("extract parameter \'$.grant_id\' from JSON body into \'grantId\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table126 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table126.AddRow(new string[] {
                            "response_type",
                            "code"});
                table126.AddRow(new string[] {
                            "client_id",
                            "fortyEightClient"});
                table126.AddRow(new string[] {
                            "state",
                            "state"});
                table126.AddRow(new string[] {
                            "response_mode",
                            "query"});
                table126.AddRow(new string[] {
                            "redirect_uri",
                            "http://localhost:8080"});
                table126.AddRow(new string[] {
                            "nonce",
                            "nonce"});
                table126.AddRow(new string[] {
                            "claims",
                            "{ \"id_token\": { \"acr\": { \"essential\" : true, \"value\": \"urn:openbanking:psd2:ca\" }" +
                                " } }"});
                table126.AddRow(new string[] {
                            "resource",
                            "https://cal.example.com"});
                table126.AddRow(new string[] {
                            "grant_management_action",
                            "create"});
                table126.AddRow(new string[] {
                            "scope",
                            "grant_management_query"});
#line 92
 testRunner.And("execute HTTP GET request \'http://localhost/authorization\'", ((string)(null)), table126, "And ");
#line hidden
#line 105
 testRunner.And("extract parameter \'code\' from redirect url", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table127 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table127.AddRow(new string[] {
                            "client_id",
                            "fortyEightClient"});
                table127.AddRow(new string[] {
                            "client_secret",
                            "password"});
                table127.AddRow(new string[] {
                            "grant_type",
                            "authorization_code"});
                table127.AddRow(new string[] {
                            "code",
                            "$code$"});
                table127.AddRow(new string[] {
                            "redirect_uri",
                            "http://localhost:8080"});
#line 107
 testRunner.And("execute HTTP POST request \'https://localhost:8080/token\'", ((string)(null)), table127, "And ");
#line hidden
#line 115
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 116
 testRunner.And("extract parameter \'$.access_token\' from JSON body into \'accessToken\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table128 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table128.AddRow(new string[] {
                            "Authorization",
                            "Bearer $accessToken$"});
#line 118
 testRunner.And("execute HTTP GET request \'http://localhost/grants/$grantId$\'", ((string)(null)), table128, "And ");
#line hidden
#line 122
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 124
 testRunner.Then("HTTP status code equals to \'401\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 125
 testRunner.And("JSON \'error\'=\'invalid_token\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 126
 testRunner.And("JSON \'error_description\'=\'the client fortyEightClient is not authorized to access" +
                        " to perform operations on the grant\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
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
                GrantErrorsFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                GrantErrorsFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
