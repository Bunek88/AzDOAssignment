# AzDOAssignment-test

First I started with some courses on Pluralsight as I never really digged into Infrastructure as Code topic, was only familiar with the name.
That way I did some training with Terraform and learned more about the principle of Infrastructure as Code.

My first solution to the Part1 - create an App service was developed in Terraform, but I stumbled on a lot of permission issues, I think that was the biggest challenge in my assignment. 

For automation I decided to go with Azure Pipelines and tried using my Visual Studio subscription that I have with my company account (to not use my free trial yet), but even though I created all the new instances for everything it was still behind the organization and some of the security policies were applied. It generated a lot of troubleshooting and workarounds, even though I started the trial on my personal account eventually(for example I couldn't run pipelines, because of some setting that I needed to submit a form for, which takes few working days to happen, and that didn't happen yet). In the end the automated solution is a mix between my personal and company infrastructures.

Since permission issues were affecting setup of the backend for terraform state file I decided to move to a technology that I am more familiar with and not spend too much time on Terraform(but developed a script that builds infrastructure and managed to run it successfuly via pipelines a couple times).
That way I ended up looking into the .Net solution.

I used the sample project that was provided with the assignment, modified it a little bit for my needs, added a feature for deleting App Services, which I commented and decided to use a PowerShell script to handle that, as I wanted to gain more experience in setting up pipelines with a variety of technologies. Since the .NET ARM Template code had a few functions I also wrote some unit tests for them.

For the simple app I went with default asp.net core web app, to do some more exercises around deploying .net apps with pipelines and because of that I wrote a small app that checks if the simple app is responsing after each deployment by sending web requests to url and reading the status code.

Pipelines itself are in Azure DevOps and first pipeline is building the Simple App and preparing a zip package for deployment (code for that can be seen in the repo), then once that's finished it triggers the release pipeline which consists of 4 stages:

- 1st stage runs .net code with ARM Template to create/update the app service
- 2nd stage deploys the .zip package to the app service
- 3rd stage runs .net code that checks if the web app is responsing
- 4th stage requires approval and once approved it deletes the app service

I really enjoyed the task, to be fair more than I expected. Since it was my first time doing an automation like that I know that there is still plenty of room for me to improve.


test
