import "../../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import * as Router from "react-router";
import { BrowserRouter } from "react-router-dom";
import { render } from "@testing-library/react";
import { PasswordResetContentDto } from "../../../Api/Models";
import { ApplicationDefault, ApplicationState } from "../../../Store/Configuration";
import { ContentPageData } from "../../../Store/Defaults";
import { PasswordReset } from "./passwordReset";

jest.mock("react-router", () => ({
    ...(jest.requireActual("react-router") as typeof Router),
}));

jest.mock("react-redux", () => ({
    ...jest.requireActual("react-redux"),
    useSelector: jest.fn(),
}));

describe("test account group component: passwordReset", () => {
    const testContent: PasswordResetContentDto = {
        language: "eng",
        caption: "Reset Password",
        button: "Reset",
        labelEmail: "Email address",
    };

    let state: ApplicationState = ApplicationDefault;
    state.contentPageData = ContentPageData;
    state.contentPageData.components.pagePasswordReset = testContent;

    const useSelectorMock = jest.spyOn(Redux, "useSelector");
    const useDispatchMock = jest.spyOn(Redux, "useDispatch");
    beforeEach(() => {
        useSelectorMock.mockImplementation(callback => callback(state));
        useDispatchMock.mockReturnValue(jest.fn());
    });

    it("should renders correctly '<PasswordReset />' when content is loaded.", () => {
        const html = render(
            <BrowserRouter>
                <PasswordReset />
            </BrowserRouter>
        );

        expect(useSelectorMock).toBeCalledTimes(8);
        expect(useDispatchMock).toBeCalledTimes(2);
        expect(html).toMatchSnapshot();
    });
});
