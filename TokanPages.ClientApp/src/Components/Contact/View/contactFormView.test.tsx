import "../../../setupTests";
import React from "react";
import { BrowserRouter } from "react-router-dom";
import { render } from "@testing-library/react";
import { ContactFormView } from "./contactFormView";

describe("test component: contactFormView", () => {
    it("should render correctly '<ContactFormView />' when content is loaded.", () => {
        const html = render(
            <BrowserRouter>
                <ContactFormView
                    isLoading={false}
                    mode="section"
                    caption="Contact me"
                    hasCaption={true}
                    hasShadow={true}
                    hasIcon={true}
                    keyHandler={jest.fn()}
                    formHandler={jest.fn()}
                    messageHandler={jest.fn()}
                    firstName="Ester"
                    lastName="Exposito"
                    email="ester.exposito@gmail.com"
                    subject="Test subject"
                    message="Test message..."
                    buttonHandler={jest.fn()}
                    progress={false}
                    buttonText="Submit"
                    consent="I agree!"
                    presentation={{
                        image: {
                            link: "",
                            title: "",
                            alt: "",
                            width: 0,
                            heigh: 0,
                        },
                        title: "",
                        subtitle: "",
                        icon: {
                            name: "",
                            href: "",
                        },
                        description: "",
                        logos: {
                            title: "",
                            images: [],
                        },
                    }}
                    labelFirstName="First name"
                    labelLastName="Last name"
                    labelEmail="Email address"
                    labelSubject="Subject"
                    labelMessage="Message"
                    minRows={undefined}
                />
            </BrowserRouter>
        );

        expect(html).toMatchSnapshot();
    });
});
