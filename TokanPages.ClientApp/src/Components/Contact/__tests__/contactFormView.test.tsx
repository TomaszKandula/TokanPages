import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import ContactFormView from "../contactFormView";

describe("Test component: contactFormView.", () => 
{
    it("Renders correctly '<ContactFormView />' when content is loaded.", () => 
    {
        const tree = shallow(<ContactFormView bind=
        {{
            isLoading: false,
            caption: "Contact me",
            text: "If you have any questions...",
            formHandler: jest.fn(),
            firstName: "Ester",
            lastName: "Exposito",
            email: "ester.exposito@gmail.com",
            subject: "Test subject",
            message: "Test message...",
            terms: false,
            buttonHandler: jest.fn(),
            progress: false,
            buttonText: "Submit"
        }}/>);
        expect(tree).toMatchSnapshot();
    });
});
