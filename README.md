# Weather Forecast Api

This repository have the pourpose of training my team in building effective tests and pass through some issues we find during the development, such as mapping errors, dependency injection, logical error, test implementation not following business and more.

Basically the application consists of consumming the [MetaWeather API](https://www.metaweather.com/api/) to get the weather by location and convert the results for the desired thermometric scale.

The tests are distributed in three layers of the bottom of the original test pyramid drawn by Martin Fowler. Follow the links for more information.

- [Test pyramid](https://martinfowler.com/bliki/TestPyramid.html)
- [Test pyramid applied to .Net](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/test-asp-net-core-mvc-apps)

![3 layers test pyramid with Functional tests at the top, followed by integration tests and unit tests on bottom](https://github.com/cboiam/WeatherForecast/raw/master/images/TestPyramid.png)

## Usage

There are two branches in this repository for training, the main objective of both are to get to the maximum code coverage and mutant killed you can get.

The `Broken` branch have some tests already implemented, with tests passing, breaking and missing. Use this to also improve your debugging skills.

The `Missing` branch doesn't have any tests implemented, use your creativity and write them in your own code style and techniques.

## Running the application

Run command inside the api project folder:

```
$ dotnet run
```

## Running the tests

Run command on the root folder or inside a test project folder:

```
$ dotnet test
```

## Running the code coverage analysis

If you don't have reportgenerator tool installed, go to the root of the project and run:

```
$ dotnet tool restore
```

On the root folder run the following commands:

```
$ dotnet test WeatherForecast.sln /p:CoverletOutputFormat=opencover /p:CoverletOutput=./TestResults/ /p:CollectCoverage=true

$ dotnet reportgenerator "-reports:./tests/WeatherForecast.UnitTest/TestResults/coverage.opencover.xml;./tests/WeatherForecast.IntegrationTest/TestResults/coverage.opencover.xml;./tests/WeatherForecast.FunctionalTest/TestResults/coverage.opencover.xml" "-targetdir:coveragereport" -reporttypes:Html
```

## Running the mutation tests

If you don't have dotnet-stryker installed, go to the root of the project and run:

```
$ dotnet tool restore
```

On WeatherForecast/tests/WeatherForecast.UnitTest run:

```
$ dotnet stryker --project-file=[projectfile]
```

## Built with

- [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1) - _Software Developmet Kit (SDK)_
- [xUnit.net](https://xunit.net/) - _Unit Test Framework_
- [Fluent Assertions](https://fluentassertions.com/) - _Unit Test Assertions Extension Methods_
- [Moq](https://github.com/moq/moq4) - _The most popular mocking library for .NET_
- [WireMock](https://github.com/WireMock-Net/WireMock.Net) - _A flexible library for stubbing and mocking web HTTP responses_

## My results

##### Test pyramid
![Test pyramid result with 2 functional tests, 5 integration tests and 23 unit tests](https://github.com/cboiam/WeatherForecast/raw/master/images/TestPyramidResult.PNG)

##### Coverage
![Coverage result with 100% coverage on core project and 93% coverage on api project](https://github.com/cboiam/WeatherForecast/raw/master/images/CoverageReport.png)

##### Mutation report of the controllers
![Mutation report with 100% killed mutations on controllers](https://github.com/cboiam/WeatherForecast/raw/master/images/MutationReportOnUnitTestPointingToApi.PNG)

##### Mutation report of the core logic
![Mutation report with 100% killed mutations on core logic](https://github.com/cboiam/WeatherForecast/raw/master/images/MutationReportOnUnitTestPointingToCore.PNG)

##### Mutation report of the services
![Mutation report with 100% killed mutations on services](https://github.com/cboiam/WeatherForecast/raw/master/images/MutationReportOnIntegrationTestPointingToCore.PNG)
