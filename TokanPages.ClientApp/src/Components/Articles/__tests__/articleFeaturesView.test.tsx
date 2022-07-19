import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { ArticleFeaturesView } from "../ArticleFeatures/view";

describe("Test articles group component: ArticleFeaturesView.", () => 
{
    it("Renders correctly '<ArticleFeaturesView />' when content is loaded.", () => 
    {
        const tree = shallow(<ArticleFeaturesView isLoading={false} content=
        {{            
            title: "Articles",
            description: "I write regularly...",
            text1: ".NET Core, Azure, databases and others.",
            text2: "Let's dive into Microsoft technology...",
            action: 
            {
                text: "View list of articles",
                href: "/action-link",
            },
            image1: "image1.jpg",
            image2: "image2.jpg",
            image3: "image3.jpg",
            image4: "image4.jpg",
        }}/>);
        expect(tree).toMatchSnapshot();
    });
});
