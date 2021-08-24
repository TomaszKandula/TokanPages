import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import HeaderView from "../headerView";

describe("Test component: headerView.", () => 
{
    it("Renders correctly '<HeaderView />' when content is loaded.", () => 
    {
        const tree = shallow(<HeaderView isLoading={false} content=
        {{
            photo: "tomek_bergen.jpg",
            caption: "Welcome to my web page",
            description: "I do programming for a living...",
            action: "Read the story"
        }}/>);
        expect(tree).toMatchSnapshot();
    });
});
