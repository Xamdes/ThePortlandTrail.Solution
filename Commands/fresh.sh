#!/usr/bin/env bash
cd ..
dotnet restore ./ThePortlandTrail
dotnet build ./ThePortlandTrail
dotnet restore ./ThePortlandTrail.Tests/
