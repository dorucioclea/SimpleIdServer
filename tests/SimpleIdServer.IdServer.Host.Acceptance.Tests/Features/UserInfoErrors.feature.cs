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
    public partial class UserInfoErrorsFeature : object, Xunit.IClassFixture<UserInfoErrorsFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "UserInfoErrors.feature"
#line hidden
        
        public UserInfoErrorsFeature(UserInfoErrorsFeature.FixtureData fixtureData, SimpleIdServer_IdServer_Host_Acceptance_Tests_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features", "UserInfoErrors", "\tCheck errors returned by the userinfo endpoint", ProgrammingLanguage.CSharp, featureTags);
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
        
        [Xunit.SkippableFactAttribute(DisplayName="access token is required (HTTP GET)")]
        [Xunit.TraitAttribute("FeatureTitle", "UserInfoErrors")]
        [Xunit.TraitAttribute("Description", "access token is required (HTTP GET)")]
        public void AccessTokenIsRequiredHTTPGET()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("access token is required (HTTP GET)", null, tagsOfScenario, argumentsOfScenario, featureTags);
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
                TechTalk.SpecFlow.Table table387 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
#line 5
 testRunner.When("execute HTTP GET request \'http://localhost/userinfo\'", ((string)(null)), table387, "When ");
#line hidden
#line 8
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 10
 testRunner.Then("JSON \'error\'=\'invalid_request\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 11
 testRunner.Then("JSON \'error_description\'=\'missing token\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="access token is required (HTTP POST)")]
        [Xunit.TraitAttribute("FeatureTitle", "UserInfoErrors")]
        [Xunit.TraitAttribute("Description", "access token is required (HTTP POST)")]
        public void AccessTokenIsRequiredHTTPPOST()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("access token is required (HTTP POST)", null, tagsOfScenario, argumentsOfScenario, featureTags);
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
                TechTalk.SpecFlow.Table table388 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
#line 14
 testRunner.When("execute HTTP POST request \'http://localhost/userinfo\'", ((string)(null)), table388, "When ");
#line hidden
#line 17
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 19
 testRunner.Then("JSON \'error\'=\'invalid_request\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 20
 testRunner.Then("JSON \'error_description\'=\'missing token\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="content-Type cannot be equals to \'application/json\'")]
        [Xunit.TraitAttribute("FeatureTitle", "UserInfoErrors")]
        [Xunit.TraitAttribute("Description", "content-Type cannot be equals to \'application/json\'")]
        public void Content_TypeCannotBeEqualsToApplicationJson()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("content-Type cannot be equals to \'application/json\'", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 22
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table389 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
#line 23
 testRunner.When("execute HTTP POST JSON request \'http://localhost/userinfo\'", ((string)(null)), table389, "When ");
#line hidden
#line 26
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 28
 testRunner.Then("JSON \'error\'=\'invalid_request\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 29
 testRunner.Then("JSON \'error_description\'=\'the content-type is not correct\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="access token must be valid")]
        [Xunit.TraitAttribute("FeatureTitle", "UserInfoErrors")]
        [Xunit.TraitAttribute("Description", "access token must be valid")]
        public void AccessTokenMustBeValid()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("access token must be valid", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 31
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table390 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table390.AddRow(new string[] {
                            "Authorization",
                            "Bearer rnd rnd"});
#line 32
 testRunner.When("execute HTTP GET request \'http://localhost/userinfo\'", ((string)(null)), table390, "When ");
#line hidden
#line 36
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 38
 testRunner.Then("JSON \'error\'=\'invalid_request\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 39
 testRunner.Then("JSON \'error_description\'=\'missing token\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="access token must be a JWT")]
        [Xunit.TraitAttribute("FeatureTitle", "UserInfoErrors")]
        [Xunit.TraitAttribute("Description", "access token must be a JWT")]
        public void AccessTokenMustBeAJWT()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("access token must be a JWT", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 41
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table391 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table391.AddRow(new string[] {
                            "Authorization",
                            "Bearer rnd"});
#line 42
 testRunner.When("execute HTTP GET request \'http://localhost/userinfo\'", ((string)(null)), table391, "When ");
#line hidden
#line 46
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 48
 testRunner.Then("JSON \'error\'=\'invalid_token\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 49
 testRunner.Then("JSON \'error_description\'=\'bad token\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="user must exists")]
        [Xunit.TraitAttribute("FeatureTitle", "UserInfoErrors")]
        [Xunit.TraitAttribute("Description", "user must exists")]
        public void UserMustExists()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("user must exists", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 51
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table392 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table392.AddRow(new string[] {
                            "sub",
                            "unknown"});
#line 52
 testRunner.Given("build access_token and sign with the key \'keyid\'", ((string)(null)), table392, "Given ");
#line hidden
                TechTalk.SpecFlow.Table table393 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table393.AddRow(new string[] {
                            "Authorization",
                            "Bearer $access_token$"});
#line 56
 testRunner.When("execute HTTP GET request \'http://localhost/userinfo\'", ((string)(null)), table393, "When ");
#line hidden
#line 60
 testRunner.Then("HTTP status code equals to \'401\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="client identifier presents in the access token must be valid")]
        [Xunit.TraitAttribute("FeatureTitle", "UserInfoErrors")]
        [Xunit.TraitAttribute("Description", "client identifier presents in the access token must be valid")]
        public void ClientIdentifierPresentsInTheAccessTokenMustBeValid()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("client identifier presents in the access token must be valid", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 62
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table394 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table394.AddRow(new string[] {
                            "sub",
                            "user"});
                table394.AddRow(new string[] {
                            "client_id",
                            "invalid"});
#line 63
 testRunner.Given("build access_token and sign with the key \'keyid\'", ((string)(null)), table394, "Given ");
#line hidden
                TechTalk.SpecFlow.Table table395 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table395.AddRow(new string[] {
                            "Authorization",
                            "Bearer $access_token$"});
#line 68
 testRunner.When("execute HTTP GET request \'http://localhost/userinfo\'", ((string)(null)), table395, "When ");
#line hidden
#line 72
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 74
 testRunner.Then("JSON \'error\'=\'invalid_client\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 75
 testRunner.Then("JSON \'error_description\'=\'unknown client invalid\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="consent must be confirmed by the end-user")]
        [Xunit.TraitAttribute("FeatureTitle", "UserInfoErrors")]
        [Xunit.TraitAttribute("Description", "consent must be confirmed by the end-user")]
        public void ConsentMustBeConfirmedByTheEnd_User()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("consent must be confirmed by the end-user", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 77
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table396 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table396.AddRow(new string[] {
                            "sub",
                            "user"});
                table396.AddRow(new string[] {
                            "client_id",
                            "thirdClient"});
                table396.AddRow(new string[] {
                            "scope",
                            "profile"});
#line 78
 testRunner.Given("build access_token and sign with the key \'keyid\'", ((string)(null)), table396, "Given ");
#line hidden
                TechTalk.SpecFlow.Table table397 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table397.AddRow(new string[] {
                            "Authorization",
                            "Bearer $access_token$"});
#line 84
 testRunner.When("execute HTTP GET request \'http://localhost/userinfo\'", ((string)(null)), table397, "When ");
#line hidden
#line 88
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 90
 testRunner.Then("JSON \'error\'=\'invalid_request\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 91
 testRunner.Then("JSON \'error_description\'=\'no consent has been accepted\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="rejected access token cannot be used")]
        [Xunit.TraitAttribute("FeatureTitle", "UserInfoErrors")]
        [Xunit.TraitAttribute("Description", "rejected access token cannot be used")]
        public void RejectedAccessTokenCannotBeUsed()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("rejected access token cannot be used", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 93
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 94
 testRunner.Given("authenticate a user", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
                TechTalk.SpecFlow.Table table398 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table398.AddRow(new string[] {
                            "response_type",
                            "code"});
                table398.AddRow(new string[] {
                            "client_id",
                            "thirtySevenClient"});
                table398.AddRow(new string[] {
                            "state",
                            "state"});
                table398.AddRow(new string[] {
                            "response_mode",
                            "query"});
                table398.AddRow(new string[] {
                            "scope",
                            "openid email role"});
                table398.AddRow(new string[] {
                            "redirect_uri",
                            "http://localhost:8080"});
                table398.AddRow(new string[] {
                            "nonce",
                            "nonce"});
                table398.AddRow(new string[] {
                            "display",
                            "popup"});
#line 95
 testRunner.When("execute HTTP GET request \'http://localhost/authorization\'", ((string)(null)), table398, "When ");
#line hidden
#line 106
 testRunner.And("extract parameter \'code\' from redirect url", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table399 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table399.AddRow(new string[] {
                            "client_id",
                            "thirtySevenClient"});
                table399.AddRow(new string[] {
                            "client_secret",
                            "password"});
                table399.AddRow(new string[] {
                            "grant_type",
                            "authorization_code"});
                table399.AddRow(new string[] {
                            "code",
                            "$code$"});
                table399.AddRow(new string[] {
                            "redirect_uri",
                            "http://localhost:8080"});
#line 108
 testRunner.And("execute HTTP POST request \'https://localhost:8080/token\'", ((string)(null)), table399, "And ");
#line hidden
#line 116
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 117
 testRunner.And("extract parameter \'access_token\' from JSON body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table400 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table400.AddRow(new string[] {
                            "token",
                            "$access_token$"});
                table400.AddRow(new string[] {
                            "client_id",
                            "thirtySevenClient"});
                table400.AddRow(new string[] {
                            "client_secret",
                            "password"});
#line 119
 testRunner.And("execute HTTP POST request \'https://localhost:8080/token/revoke\'", ((string)(null)), table400, "And ");
#line hidden
                TechTalk.SpecFlow.Table table401 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table401.AddRow(new string[] {
                            "Authorization",
                            "Bearer $access_token$"});
#line 125
 testRunner.And("execute HTTP GET request \'http://localhost/userinfo\'", ((string)(null)), table401, "And ");
#line hidden
#line 129
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 131
 testRunner.Then("JSON \'error\'=\'invalid_token\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 132
 testRunner.Then("JSON \'error_description\'=\'access token has been rejected\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
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
                UserInfoErrorsFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                UserInfoErrorsFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion