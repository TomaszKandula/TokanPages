import "../../../../setupTests";
import React from "react";
import { BrowserRouter } from "react-router-dom";
import { render } from "enzyme";
import { ResetPasswordView } from "./resetPasswordView";

describe("test account group component: resetPasswordView", () => {
    it("should render correctly '<ResetPasswordView />' when content is loaded.", () => {
        const html = render(
            <BrowserRouter>
                <ResetPasswordView
                    isLoading={false}
                    progress={false}
                    caption="Reset Password"
                    button="Reset"
                    email="tokan@dfds.com"
                    keyHandler={jest.fn()}
                    formHandler={jest.fn()}
                    buttonHandler={jest.fn()}
                    labelEmail="Email address"
                />
            </BrowserRouter>
        );

        expect(html).toMatchSnapshot();
    });
});
