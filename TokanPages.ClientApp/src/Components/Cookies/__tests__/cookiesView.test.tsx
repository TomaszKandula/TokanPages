import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import CookiesView from "../cookiesView";

describe("Test component: cookiesView.", () => 
{
    it("Renders correctly '<CookiesView />' when content is loaded.", () => 
    {
        const tree = shallow(<CookiesView bind=
        {{
            isLoading: false,
            modalClose: false,
            shouldShow: false,
            caption: "Cookie Policy",
            text: "We use cookies to personalise content...",
            onClickEvent: jest.fn(),
            buttonText: "Accept cookies"
        }}/>);
        expect(tree).toMatchSnapshot();
    });
});
