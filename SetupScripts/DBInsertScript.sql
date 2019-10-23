USE [MissionControlApp]
GO

INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (1, 2)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (7, 2)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (1, 5)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (7, 5)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (11, 5)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (1, 6)
GO

INSERT [dbo].[BusinessFunctions] ([BusinessFunctionName], [BusinessFunctionAlias], [DateCreated], [Active]) VALUES (N'Marketing', N'Marketing', CAST(N'2019-05-29T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[BusinessFunctions] ([BusinessFunctionName], [BusinessFunctionAlias], [DateCreated], [Active]) VALUES (N'Accounting', N'Accounting', CAST(N'2019-06-14T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[BusinessFunctions] ([BusinessFunctionName], [BusinessFunctionAlias], [DateCreated], [Active]) VALUES (N'Other', N'Other', CAST(N'2019-06-24T11:23:43.3700000' AS DateTime2), 1)
GO

INSERT [dbo].[Industries] ([IndustryName], [IndustryAlias], [DateCreated], [Active]) VALUES (N'Energy', N'Energy', CAST(N'2019-05-29T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Industries] ([IndustryName], [IndustryAlias], [DateCreated], [Active]) VALUES (N'Manufacturing', N'Manufacturing', CAST(N'2019-06-24T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Industries] ([IndustryName], [IndustryAlias], [DateCreated], [Active]) VALUES (N'Other', N'Other', CAST(N'2019-06-24T11:20:01.7100000' AS DateTime2), 1)
GO

INSERT [dbo].[Accelerators] ([BusinessFunctionId], [IndustryId], [AcceleratorName], [ModelType], [DateCreated], [Active], [Description]) VALUES (1, 1, N'Sentiment', N'Predictive', CAST(N'2019-05-29T00:00:00.0000000' AS DateTime2), 1, N'{"name":"Sentiment Analysis", "description":"Sentiment Analysis",
"commonuses":["the most common uses are the following","common uses 2"],
"visuals":[{"imglocation":"http://wwww.stuff.com", "description": "some description"}],
"benefits":["benefit 1", "benefit 2", "benefit 3"]
}')
GO
INSERT [dbo].[Accelerators] ([BusinessFunctionId], [IndustryId], [AcceleratorName], [ModelType], [DateCreated], [Active], [Description]) VALUES (1, 1, N'Audience Targeting', N'Predictive', CAST(N'2019-05-29T00:00:00.0000000' AS DateTime2), 1, N'{"name":"Audience Targeting", 
"description":"Audience Targeting",
"commonuses":["the most common uses are the following","common uses 2"],
"visuals":[{"imglocation":"http://wwww.stuff.com", "description": "some description"}],
"benefits":["benefit 1", "benefit 2", "benefit 3"]
}')
GO
INSERT [dbo].[Accelerators] ([BusinessFunctionId], [IndustryId], [AcceleratorName], [ModelType], [DateCreated], [Active], [Description]) VALUES (2, 1, N'Forecasting', N'Forecast', CAST(N'2019-06-17T00:00:00.0000000' AS DateTime2), 1, N'{"name":"Forecasting", 
"description":"Forecasting",
"commonuses":["the most common uses are the following","common uses 2"],
"visuals":[{"imglocation":"http://wwww.stuff.com", "description": "some description"}],
"benefits":["benefit 1", "benefit 2", "benefit 3"]
}')
GO
INSERT [dbo].[Accelerators] ([BusinessFunctionId], [IndustryId], [AcceleratorName], [ModelType], [DateCreated], [Active], [Description]) VALUES (2, 1, N'Risk', N'Risk', CAST(N'2019-06-17T00:00:00.0000000' AS DateTime2), 1, N'{"name":"Risk Prediction", "description":"Risk Prediction",
"commonuses":["the most common uses are the following","common uses 2"],
"visuals":[{"imglocation":"http://wwww.stuff.com", "description": "some description"}],
"benefits":["benefit 1", "benefit 2", "benefit 3"]
}')
GO

INSERT [dbo].[MissionStatus] ([MissionStatusName], [MissionStatusCode], [MissionStatusAlias], [Sequence], [Description], [MissionStatusType], [DateCreated], [Active]) VALUES (N'inprocess', N'inprocess', N'In-Process', 1, N'In-Process indicates that a mission been submitted and is in process.', NULL, CAST(N'2019-09-05T00:00:00.0000000' AS DateTime2), 1)
GO

INSERT [dbo].[Platforms] ([PlatformName], [PlatformAlias], [Description], [Type], [DateCreated], [Active]) VALUES (N'AWSSageMaker', N'AWS SageMaker', N'AWS SageMaker Machine Learning Service', N'AI', CAST(N'2019-06-07T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Platforms] ([PlatformName], [PlatformAlias], [Description], [Type], [DateCreated], [Active]) VALUES (N'AzureML', N'Azure Machine Learning', N'Azure Machine Learning Service', N'AI', CAST(N'2019-06-07T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Platforms] ([PlatformName], [PlatformAlias], [Description], [Type], [DateCreated], [Active]) VALUES (N'GCP', N'GCP', N'Google Cloud Platform', N'Cloud', CAST(N'2019-06-07T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Platforms] ([PlatformName], [PlatformAlias], [Description], [Type], [DateCreated], [Active]) VALUES (N'AliCloud', N'AliCloud', N'Alibaba Cloud', N'Cloud', CAST(N'2019-06-07T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Platforms] ([PlatformName], [PlatformAlias], [Description], [Type], [DateCreated], [Active]) VALUES (N'DataBricks', N'DataBricks', N'Databricks is an AI platform', N'AI', CAST(N'2019-06-07T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Platforms] ([PlatformName], [PlatformAlias], [Description], [Type], [DateCreated], [Active]) VALUES (N'Other', N'Other', N'Any other platform used for AI', N'Other', CAST(N'2019-06-07T00:00:00.0000000' AS DateTime2), 1)
GO