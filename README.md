# Hacker News API Repository

This repository is a ASP.NET Core 8.0 Web API Project that provides an API for retrieving the details of the best n stories from the Hacker News API sorted by their score, as the caller can specify the number of the requested stories by passing the **numberOfStories** parameter to the API endpoint.

## How to run?

- Clone repository.
- build and publish the project.
- host the release on a web server.
- Then the caller can call the URL which is a combination of the Base URL and this endpoint: **/api/HN/GetStories**

## Example

Assums that the developer hosted the project on IIS Server, on port: 5276, so the caller can call this URL:
http://localhost:5276/api/HN/GetStories?numberOfStories=2
to get the details of the top 2 stories:


[{"title": "In loving memory of square checkbox",        
        "uri": "https://tonsky.me/blog/checkbox/",        
        "postedBy": "kevingadd",        
        "time": "2024-01-28T00:36:19",        
        "score": 1961,        
        "commentCount": 503        
    },        
    {
        "title": "Infinite Craft",        
        "uri": "https://neal.fun/infinite-craft/",        
        "postedBy": "kretaceous",        
        "time": "2024-01-31T15:34:28",        
        "score": 1161,        
        "commentCount": 552        
    }]

## Code Remarks
I created a seperate API Service that allows me to make a GET request for the Hacker News API.
This service contains two method:
- **CreateSingleRequest:** By calling this method, I can make a single request to the API.
- **CreateRequests:** By calling this method, I can make a set of requests by passing an array of API endpoints. this won't return the responses unless all tasks are completed.



