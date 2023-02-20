import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { NewsletterView } from "./newsletterView";

describe("test component: newsletterView", () => 
{
    it("should render correctly '<NewsletterView />' when content is loaded.", () => 
    {
        const tree = shallow(<NewsletterView
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
        />);

        expect(tree).toMatchSnapshot();
    });
});
