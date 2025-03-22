import "../../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import * as Dom from "react-router-dom";
import { render } from "@testing-library/react";
import { AccountActivateContentDto } from "../../../Api/Models";
import { ContentPageData } from "../../../Store/Defaults";
import { ApplicationDefault, ApplicationState } from "../../../Store/Configuration";
import { AccountActivate } from "./accountActivate";

jest.mock("react-router-dom", () => ({
    ...(jest.requireActual("react-router-dom") as typeof Dom),
    useLocation: () => ({
        search: "localhost:3000/accountactivation/?id=dba4043c-7428-4f72-ba13-fe782c7a88fa",
    }),
}));

jest.mock("react-redux", () => ({
    ...jest.requireActual("react-redux"),
    useSelector: jest.fn(),
}));

describe("Test account group component: accountActivate", () => {
    const testId = "dba4043c-7428-4f72-ba13-fe782c7a88fa";
    const testContent: AccountActivateContentDto = {
        language: "eng",
        onVerifying: {
            type: "Verifying",
            caption: "Email Verification",
            text1: "Processing your account..., please wait.",
            text2: "",
            button: "",
        },
        onProcessing: {
            type: "Processing",
            caption: "Account Activation",
            text1: "Processing your account..., please wait.",
            text2: "",
            button: "",
        },
        onSuccess: {
            type: "Success",
            caption: "Account Processing",
            button: "Go to main",
            noBusinessLock: {
                text1: "Your account has been successfully processed!",
                text2: "You can now use all our services.",
            },
            businessLock: {
                text1: "Your account has been <b>partially activated</b>.",
                text2: "Please wait for our decision. You will hear from us soon.",
            },
        },
        onError: {
            type: "Error",
            caption: "Processing Error",
            text1: "Could not process your account. Unexpected error occurred.",
            text2: "Please contact IT support.",
            button: "Retry",
        },
    };

    let state: ApplicationState = ApplicationDefault;
    state.contentPageData = ContentPageData;
    state.contentPageData.components.accountActivate = testContent;

    const useSelectorMock = jest.spyOn(Redux, "useSelector");
    const useDispatchMock = jest.spyOn(Redux, "useDispatch");
    beforeEach(() => {
        useSelectorMock.mockImplementation(callback => callback(state));
        useDispatchMock.mockReturnValue(jest.fn());
    });

    it("should render correctly '<AccountActivate />' when content is loaded.", () => {
        const html = render(<AccountActivate id={testId} type="" />);
        expect(useSelectorMock).toBeCalledTimes(9);
        expect(useDispatchMock).toBeCalledTimes(3);
        expect(html).toMatchSnapshot();
    });
});
