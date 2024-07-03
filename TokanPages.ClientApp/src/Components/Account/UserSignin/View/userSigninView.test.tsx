import "../../../../setupTests";
import React from "react";
import { render } from "enzyme";
import { UserSigninView } from "./userSigninView";

describe("test account group component: userSigninView", () => {
    it("should render correctly '<UserSigninView />' when content is loaded.", () => {
        const html = render(
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
        );

        expect(html).toMatchSnapshot();
    });
});
