import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import UnsubscribeView from "../unsubscribeView";

describe("Test component: unsubscribeView.", () => 
{
    it("Renders correctly '<UnsubscribeView />' when content is loaded.", () => 
    {
        const tree = shallow(<UnsubscribeView bind=
        {{
            isLoading: false,
            caption: "Cancel your subscribtion",
            text1: "We are sorry to see you go...",
            text2: "...but we understand there are circumstances",
            text3: "Please contact us should you have any questions",
            buttonHandler: jest.fn(),
            buttonState: false,
            progress: false,
            buttonText: "Unsubscribe Now"
        }}/>);
        expect(tree).toMatchSnapshot();
    });
});
