import "../../../../setupTests";
import React from "react";
import { BrowserRouter } from "react-router-dom";
import { render } from "@testing-library/react";
import { UserSignupView } from "./userSignupView";

describe("test account group component: userSignupView", () => {
    it("should render correctly '<UserSignupView />' when content is loaded.", () => {
        const html = render(
            <BrowserRouter>
                <UserSignupView
                    isLoading={false}
                    caption="Create a new account"
                    warning={{
                        textPre: "Password requirements:",
                        textPost: "We recommend to use password generator...",
                        textList: ["between 15..50 characters","minimum one large letter","minimum one small letter","minimum one number"],
                        textNist: {
                            text: "FBI on passphrases and account protection",
                            href: "https://www.fbi.gov"
                        },
                    }}
                    consent="I agree to the terms of use and privacy policy."
                    button="Sign up"
                    link={{
                        text: "Already have an account? Sign in",
                        href: "/signin",
                    }}
                    buttonHandler={jest.fn()}
                    keyHandler={jest.fn()}
                    formHandler={jest.fn()}
                    progress={false}
                    firstName="Ester"
                    lastName="Exposito"
                    email="ester.exposito@gmail.com"
                    password="madrilena123"
                    terms={false}
                    labelFirstName="First name"
                    labelLastName="Last name"
                    labelEmail="Email address"
                    labelPassword="Password"
                />
            </BrowserRouter>
        );

        expect(html).toMatchSnapshot();
    });
});
