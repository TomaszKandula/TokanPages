import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import FeaturedView from "../featuredView";

describe("Test component: featuredView.", () => 
{
    it("Renders correctly '<FeaturedView />' when content is loaded.", () => 
    {
        const tree = shallow(<FeaturedView isLoading={false} content=
        {{
            caption: "Featured",
            text: "I picked three articles that I wrote...",
            title1: "Memory Management",
            subtitle1: "The basics you should know, when writing an application...",
            link1: "https://.../net-memory-management-64389b01f642",
            image1: "https://.../images/section_featured/article0.jpg",
            title2: "Stored Procedures",
            subtitle2: "I explain why I do not need them that much...",
            link2: "https://.../i-said-goodbye-to-stored-procedures-539d56350486",
            image2: "https://.../images/section_featured/article1.jpg",
            title3: "SQL Injection",
            subtitle3: "This article will explore the issue in greater detail...",
            link3: "https://.../sql-injection-1bde8bb76ebc",
            image3: "https://.../images/section_featured/article2.jpg"
        }}/>);
        expect(tree).toMatchSnapshot();
    });
});
