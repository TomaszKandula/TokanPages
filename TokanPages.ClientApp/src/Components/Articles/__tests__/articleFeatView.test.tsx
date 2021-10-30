import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import ArticleFeatView from "../articleFeatView";

describe("Test articles group component: articleFeatView.", () => 
{
    it("Renders correctly '<ArticleFeatView />' when content is loaded.", () => 
    {
        const tree = shallow(<ArticleFeatView isLoading={false} content=
        {{            
            title: "Articles",
            desc: "I write regularly...",
            text1: ".NET Core, Azure, databases and others.",
            text2: "Let's dive into Microsoft technology...",
            button: "View list of articles",
            image1: "image1.jpg",
            image2: "image2.jpg",
            image3: "image3.jpg",
            image4: "image4.jpg",
        }}/>);
        expect(tree).toMatchSnapshot();
    });
});
