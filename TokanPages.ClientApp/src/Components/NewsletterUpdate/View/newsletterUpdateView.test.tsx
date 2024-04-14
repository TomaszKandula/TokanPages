import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { NewsletterUpdateView } from "./newsletterUpdateView";

describe("test component: newsletterUpdateView", () => {
    it("should render correctly '<NewsletterUpdateView />' when content is loaded.", () => {
        const tree = shallow(
            <NewsletterUpdateView
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
