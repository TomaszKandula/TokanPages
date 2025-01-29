import "../../../../setupTests";
import React from "react";
import { BrowserRouter } from "react-router-dom";
import { render } from "@testing-library/react";
import { PasswordUpdateView } from "./passwordUpdateView";

describe("test account group component: updatePasswordView", () => {
    it("should render correctly '<PasswordUpdateView />' when content is loaded.", () => {
        const html = render(
            <BrowserRouter>
                <PasswordUpdateView
                    isLoading={false}
                    progress={false}
                    caption="Update password"
                    button="Submit"
                    newPassword="user1password"
                    verifyPassword="user1password"
                    keyHandler={jest.fn()}
                    formHandler={jest.fn()}
                    buttonHandler={jest.fn()}
                    disableForm={false}
                    labelNewPassword="New password"
                    labelVerifyPassword="Verify password"
                />
            </BrowserRouter>
        );

        expect(html).toMatchSnapshot();
    });
});
