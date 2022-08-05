import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { NewsletterView } from "./newsletterView";

describe("Test component: newsletterView.", () => 
{
    it("Renders correctly '<NewsletterView />' when content is loaded.", () => 
    {
        const tree = shallow(<NewsletterView bind=
        {{
            isLoading: false,
            caption: "Join the newsletter!",
            text: "We will never share your email address.",
            formHandler: jest.fn(),
            email: "ester.exposito@gmail.com",
            buttonHandler: jest.fn(),
            progress: false,
            buttonText: "Subscribe",
            labelEmail: "Email address"
        }}/>);
        expect(tree).toMatchSnapshot();
    });
});
