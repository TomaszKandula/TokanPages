import "../../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import { BrowserRouter as Router } from "react-router-dom";
import { render } from "@testing-library/react";
import { AccountUserSignupContentDto } from "../../../Api/Models";
import { ApplicationDefault, ApplicationState } from "../../../Store/Configuration";
import { ContentPageData } from "../../../Store/Defaults";
import { UserSignup } from "./userSignup";

jest.mock("react-redux", () => ({
    ...jest.requireActual("react-redux"),
    useSelector: jest.fn(),
}));

describe("test account group component: userSignup", () => {
    const testContent: AccountUserSignupContentDto = {
        language: "en",
        caption: "Create a new account",
        button: "Sign up",
        link: {
            text: "Already have an account? Sign in",
            href: "/account/signin"
        },
        warning: {
            textPre: "Password requirements:",
            textList: ["between 15..50 characters","minimum one large letter","minimum one small letter","minimum one number"],
            textPost: [
                "We recommend to use password generator to generate very strong and long password; to be used within browser password manager (1Password, Bitwarden, KeePass).",
                "Recommendation based on the following reading:"
            ],
            textNist: {
                text: "Strong Passphrases and Account Protection",
                href: "https://www.fbi.gov/contact-us/field-offices/elpaso/news/fbi-tech-tuesday-strong-passphrases-and-account-protection"
            }
        },
        consent: "I agree to the terms of use and privacy policy.",
        labelFirstName: "First name",
        labelLastName: "Last name",
        labelEmail: "Email address",
        labelPassword: "Password"
    };

    let state: ApplicationState = ApplicationDefault;
    state.contentPageData = ContentPageData;
    state.contentPageData.components.accountUserSignup = testContent;

    const useDispatchMock = jest.spyOn(Redux, "useDispatch");
    const useSelectorMock = jest.spyOn(Redux, "useSelector");
    beforeEach(() => {
        useSelectorMock.mockImplementation((callback) => callback(state));
        useDispatchMock.mockReturnValue(jest.fn());
    });

    it("should render correctly '<UserSignup />' when content is loaded.", () => {
        const html = render(
            <Router>
                <UserSignup />
            </Router>
        );

        expect(useDispatchMock).toBeCalledTimes(2);
        expect(useSelectorMock).toBeCalledTimes(7);
        expect(html).toMatchSnapshot();
    });
});
