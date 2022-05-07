![Screenshot](https://raw.githubusercontent.com/riganti/dotvvm-samples-businesspack4/master/images/bp001.png)

# DotVVM Business Pack 4 enhancements

This sample shows some new features introduced in [DotVVM Business Pack 4.0](https://www.dotvvm.com/products/dotvvm-business-pack). 

## Prerequisites
* Make sure you have installed [DotVVM for Visual Studio](https://www.dotvvm.com/install)

## How to run the sample

1. [Open the GitHub repo in Visual Studio](git-client://clone/?repo=https%3A%2F%2Fgithub.com%2Friganti%2Fdotvvm-samples-businesspack4)
or 
`git clone https://github.com/riganti/dotvvm-samples-businesspack4.git`

2. Open `src/BPSamples/BPSamples.sln` 
![Open the solution file](https://raw.githubusercontent.com/riganti/dotvvm-samples-businesspack4/master/images/bp002.png)

3. Make sure you have a license for [DotVVM Business Pack](https://www.dotvvm.com/products/dotvvm-business-pack) or you have [requested a free 30-days trial](https://www.dotvvm.com/login).

4. Open the **Tools > NuGet Package Manager > Package Manager Settings** and make sure you have the **DotVVM Private NuGet Feed** configured. If not, follow the steps to [Configure DotVVM Private NuGet Feed](https://www.dotvvm.com/docs/4.0/pages/dotvvm-for-visual-studio/dotvvm-private-nuget-feed#sign-in-to-add-the-feed-automatically). 
![Open the NuGet Package Manager menu](https://raw.githubusercontent.com/riganti/dotvvm-samples-businesspack4/master/images/bp003.png)
![Open the NuGet Package Manager menu](https://raw.githubusercontent.com/riganti/dotvvm-samples-businesspack4/master/images/bp003_2.png)


## Sample 1: Customize Business Pack theme using CSS variables

1. Right-click the `BPSamples.CustomProperties` project and select **View > View in Browser**
![View BPSamples.CustomProperties in Browser](https://raw.githubusercontent.com/riganti/dotvvm-samples-businesspack4/master/images/bp004.png)

2. You can see the Business Pack components using the default style. Click on the checkbox to see the overriden style.
You can find the overriden styles in the `src\BPSamples.CustomProperties\wwwroot\css\style.css` file.

![Default and overriden look of the application](https://raw.githubusercontent.com/riganti/dotvvm-samples-businesspack4/master/images/bp005.png)

3. You can try switching to the **Bootstrap 4** theme by changing the theme in the `src\BPSamples.CustomProperties\DotvvmStartup.cs` file in the `ConfigureServices` method.

![Default and overriden look of the application with the Bootstrap4 theme](https://raw.githubusercontent.com/riganti/dotvvm-samples-businesspack4/master/images/bp006.png)

## Sample 2: Customize Excel exports from the GridView 

1. Right-click the `BPSamples.GridViewExporting` project and select **View > View in Browser**
![View BPSamples.GridViewExporting in Browser](https://raw.githubusercontent.com/riganti/dotvvm-samples-businesspack4/master/images/bp007.png)

2. The application will auto-generate a sample data set. When you click the **Export** button, the Excel file will be generated.
You can find the configuration of the export in `src\BPSamples.GridViewExporting\ViewModels\DefaultViewModel.cs` file.

![Exported Excel file](https://raw.githubusercontent.com/riganti/dotvvm-samples-businesspack4/master/images/bp008.png)


## Sample 3: Using the new DotVVM.BusinessPack.Messaging package

1. Right-click the `BPSamples.Chat` project and select **View > View in Browser**
![View BPSamples.Chat in Browser](https://raw.githubusercontent.com/riganti/dotvvm-samples-businesspack4/master/images/bp009.png)

2. Repeate the same action again to open the app in a second tab.

3. Enter a different name in each tab.

4. Write chat messages and see how they are synchronized between the two tabs in the real-time.

![Chat application in action](https://raw.githubusercontent.com/riganti/dotvvm-samples-businesspack4/master/images/bp010.png)



## What you can learn in the sample

* Using CSS variables to override DotVVM Business Pack look & feel
* How to customize Excel exports in GridView
* How to use the new `DotVVM.BusinessPack.Messaging` package 

---

## Other resources

* [Gitter Chat](https://gitter.im/riganti/dotvvm)
* [DotVVM Official Website](https://www.dotvvm.com)
* [DotVVM Documentation](https://www.dotvvm.com/docs)
* [DotVVM GitHub](https://github.com/riganti/dotvvm)
* [Twitter @dotvvm](https://twitter.com/dotvvm)
* [Samples](https://www.dotvvm.com/samples)
