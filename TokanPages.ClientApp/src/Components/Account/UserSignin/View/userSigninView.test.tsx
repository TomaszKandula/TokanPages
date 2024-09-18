import "../../../../setupTests";
import React from "react";
import { BrowserRouter } from "react-router-dom";
import { render } from "@testing-library/react";
import { UserSigninView } from "./userSigninView";

describe("test account group component: userSigninView", () => {
    it("should render correctly '<UserSigninView />' when content is loaded.", () => {
        const html = render(
            <BrowserRouter>
                <UserSigninView
                    isLoading={false}
                    caption="Sign in"
                    button="Sign in"
                    link1="Don't have an account?"
                    link2="Forgot password?"
                    buttonHandler={jest.fn()}
                    progress={false}
                    keyHandler={jest.fn()}
                    formHandler={jest.fn()}
                    email="ester.exposito@gmail.com"
                    password="madrilena123"
                    labelEmail="Email address"
                    labelPassword="Password"
                />
            </BrowserRouter>
        );

        expect(html).toMatchSnapshot();
    });
});
