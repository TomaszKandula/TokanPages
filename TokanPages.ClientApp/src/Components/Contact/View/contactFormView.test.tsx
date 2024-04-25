import "../../../setupTests";
import React from "react";
import { render } from "enzyme";
import { ContactFormView } from "./contactFormView";

describe("test component: contactFormView", () => {
    it("should render correctly '<ContactFormView />' when content is loaded.", () => {
        const html = render(
            <ContactFormView
                isLoading={false}
                caption="Contact me"
                text="If you have any questions..."
                keyHandler={jest.fn()}
                formHandler={jest.fn()}
                firstName="Ester"
                lastName="Exposito"
                email="ester.exposito@gmail.com"
                subject="Test subject"
                message="Test message..."
                terms={false}
                buttonHandler={jest.fn()}
                progress={false}
                buttonText="Submit"
                consent="I agree!"
                labelFirstName="First name"
                labelLastName="Last name"
                labelEmail="Email address"
                labelSubject="Subject"
                labelMessage="Message"
            />
        );

        expect(html).toMatchSnapshot();
    });
});
