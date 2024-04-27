import "../../../setupTests";
import React from "react";
import { render } from "enzyme";
import { NewsletterView } from "./newsletterView";

describe("test component: newsletterView", () => {
    it("should render correctly '<NewsletterView />' when content is loaded.", () => {
        const html = render(
            <NewsletterView
                isLoading={false}
                caption={"Join the newsletter!"}
                text={"We will never share your email address."}
                keyHandler={jest.fn()}
                formHandler={jest.fn()}
                email={"ester.exposito@gmail.com"}
                buttonHandler={jest.fn()}
                progress={false}
                buttonText={"Subscribe"}
                labelEmail={"Email address"}
            />
        );

        expect(html).toMatchSnapshot();
    });
});
