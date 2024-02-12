import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { UpdateSubscriberView } from "./updateSubscriberView";

describe("test component: updateSubscriberView", () => {
    it("should render correctly '<UpdateSubscriberView />' when content is loaded.", () => {
        const tree = shallow(
            <UpdateSubscriberView
                isLoading={false}
                caption={"Update subscription email"}
                formHandler={jest.fn()}
                email={"ester.exposito@gmail.com"}
                buttonHandler={jest.fn()}
                buttonState={false}
                progress={false}
                buttonText={"Update"}
                labelEmail={"Email address"}
            />
        );

        expect(tree).toMatchSnapshot();
    });
});
