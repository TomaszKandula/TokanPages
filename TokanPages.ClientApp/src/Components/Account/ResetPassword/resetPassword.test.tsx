import "../../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import * as Router from "react-router";
import { render } from "enzyme";
import { ResetPassword } from "./resetPassword";

jest.mock("react-router", () => ({
    ...(jest.requireActual("react-router") as typeof Router),
}));

jest.mock("react-redux", () => ({
    ...jest.requireActual("react-redux"),
    useSelector: jest.fn(),
}));

describe("test account group component: resetPassword", () => {
    const testContent = {
        language: "eng",
        caption: "Reset Password",
        button: "Reset",
        labelEmail: "Email address",
    };

    const useDispatchMock = jest.spyOn(Redux, "useDispatch");
    beforeEach(() => {
        jest.spyOn(Redux, "useSelector").mockReturnValueOnce({
            isLoading: false,
            content: testContent,
        });

        useDispatchMock.mockReturnValue(jest.fn());
    });

    it("should renders correctly '<ResetPassword />' when content is loaded.", () => {
        const html = render(<ResetPassword />);
        expect(useDispatchMock).toBeCalledTimes(1);
        expect(html).toMatchSnapshot();
    });
});
