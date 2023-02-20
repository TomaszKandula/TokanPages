import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { UnsubscribeView } from "./unsubscribeView";
import { ContentDto } from "../../../Api/Models";

describe("test component: unsubscribeView", () => 
{
    it("should render correctly '<UnsubscribeView />' when content is loaded.", () => 
    {
        const content: ContentDto = 
        {
            caption: "Cancel your subscribtion",
            text1: "We are sorry to see you go...",
            text2: "...but we understand there are circumstances",
            text3: "Please contact us should you have any questions",
            button: "Unsubscribe"
        }

        const tree = shallow(<UnsubscribeView
            isLoading={false}
            buttonHandler={jest.fn()}
            buttonState={false}
            progress={false}
            contentPre={content}
            contentPost={content}
            isRemoved={false}
        />);

        expect(tree).toMatchSnapshot();
    });
});
