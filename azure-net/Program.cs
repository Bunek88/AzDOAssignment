// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using a.Utilities;
using System;
using System.Threading.Tasks;

namespace DeployUsingARMTemplate
{
    public class Program
    {
        public static async Task RunSample(TokenCredential credential)
        {
            var rgName = "bartbuntestsandbox001";
            var deploymentName = Utilities.RandomResourceName("dpRSAT", 24); ;
            var location = "uksouth";
            var subscriptionId = Environment.GetEnvironmentVariable("AZURE_SUBSCRIPTION_ID");
            var templateJson = Utilities.GetArmTemplate("ArmTemplate.json");

            var resourceClient = new ResourcesManagementClient(subscriptionId, credential);
            var resourceGroups = resourceClient.ResourceGroups;
            var deployments = resourceClient.Deployments;


            try
            {
                // Check if exists or Create resource group.



                var resourceGroup = new ResourceGroup(location);

                var checkRG = await resourceGroups.CheckExistenceAsync(rgName);


                if (checkRG.Status == 204)
                {
                    Utilities.Log("Resource group exists: " + rgName);
                }
                else if (checkRG.Status == 404)
                {
                    Utilities.Log("Creating a resource group with name: " + rgName);
                    resourceGroup = await resourceGroups.CreateOrUpdateAsync(rgName, resourceGroup);
                    Utilities.Log("Created a resource group with name: " + rgName);
                }


                // Create a deployment for an Azure App Service via an ARM
                // template.

                Utilities.Log("Starting a deployment for an Azure App Service: " + deploymentName);

                var parameters = new Deployment
                (
                    new DeploymentProperties(DeploymentMode.Incremental)
                    {
                        Template = templateJson,
                        Parameters = "{}"
                    }
                 );
                var rawResult = await deployments.StartCreateOrUpdateAsync(rgName, deploymentName, parameters);

                await rawResult.WaitForCompletionAsync();

                Utilities.Log("Completed the deployment: " + deploymentName);
            }
            finally
            {
                try
                {
                    //I wanted to include deleting with the ARM template, so using it on deployments as it's not affecting the task in any way 
                    Utilities.Log("Deleting Deployment: " + deploymentName);

                    await (await deployments.StartDeleteAsync(rgName, deploymentName)).WaitForCompletionAsync();

                    Utilities.Log("Deleted Deployment: " + deploymentName);

                    //deleting resource group with ARM Template
                    //Utilities.Log("Deleting Resource Group: " + rgName);
                    //await (await resourceGroups.StartDeleteAsync(rgName)).WaitForCompletionAsync();
                    //Utilities.Log("Deleted Resource Group: " + rgName);

                    //deleting appservice with ARM Template
                    //var resource = resourceClient.Resources;
                    //var resourcesList = await Utilities.ToEnumerableAsync(resource.ListByResourceGroupAsync(rgName));
                    //var appID = "";
                    //var appName = "";

                    //for (int i = 0; i < resourcesList.Count; i++)
                    //{
                    //looking for an id of a web app with a specific name
                    //    if (resourcesList[i].Kind == "app" && resourcesList[i].Name == "bartbuntest001")
                    //    {
                    //        appID = resourcesList[i].Id;
                    //    }
                    //}
                    
                    //if(appName != string.Empty && appID != string.Empty)
                    //  {
                    //      Utilities.Log("Deleting App Service: " + appName);
                    //      await resource.StartDeleteByIdAsync(appID, "2021-03-01");
                    //      Utilities.Log("Deleted App Service: " + appName);
                    //  }

                }
                catch (Exception ex)
                {
                    Utilities.Log(ex);
                }
            }
        }

        public static async Task Main(string[] args)
        {
            try
            {
                // Authenticate
                var credentials = new DefaultAzureCredential();

                await RunSample(credentials);
            }
            catch (Exception ex)
            {
                Utilities.Log(ex);
            }
        }

    }

}
