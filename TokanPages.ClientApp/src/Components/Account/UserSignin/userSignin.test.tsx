import "../../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import * as Router from "react-router";
import { BrowserRouter } from "react-router-dom";
import { render } from "@testing-library/react";
import { AccountUserSigninContentDto } from "../../../Api/Models";
import { ApplicationDefault, ApplicationState } from "../../../Store/Configuration";
import { ContentPageData } from "../../../Store/Defaults";
import { UserSignin } from "./userSignin";

jest.mock("react-router", () => ({
    ...(jest.requireActual("react-router") as typeof Router),
    useHistory: () => jest.fn(),
}));

jest.mock("react-redux", () => ({
    ...jest.requireActual("react-redux"),
    useSelector: jest.fn(),
}));

describe("test account group component: userSignin", () => {
    const testContent: AccountUserSigninContentDto = {
        language: "eng",
        caption: "Sign in",
        button: "Sign in",
        link1: {
            text: "Don't have an account?",
            href: "https://localhost/signup",
        },
        link2: {
            text: "Forgot password?",
            href: "https://localhost/reset-password",
        },
        labelEmail: "Email address",
        labelPassword: "Password",
        consent: "By signing in to an account...",
        securityNews: [
            {
                image: "news_01.webp",
                tags: ["important"],
                date: "20205-08-08",
                title: "Be careful...",
                lead: "Do not...",
                link: {
                    text: "Read more",
                    href: "/security-news",
                },
            },
        ],
    };

    let state: ApplicationState = ApplicationDefault;
    state.contentPageData = ContentPageData;
    state.contentPageData.components.accountUserSignin = testContent;

    const useSelectorMock = jest.spyOn(Redux, "useSelector");
    const useDispatchMock = jest.spyOn(Redux, "useDispatch");
    beforeEach(() => {
        useSelectorMock.mockImplementation(callback => callback(state));
        useDispatchMock.mockReturnValue(jest.fn());
    });

    it("should render correctly '<UserSignin />' when content is loaded.", () => {
        const html = render(
            <BrowserRouter>
                <UserSignin />
            </BrowserRouter>
        );

        expect(useSelectorMock).toBeCalledTimes(6);
        expect(useDispatchMock).toBeCalledTimes(1);
        expect(html).toMatchSnapshot();
    });
});
