import "../../../../setupTests";
import React from "react";
import { BrowserRouter } from "react-router-dom";
import { render } from "@testing-library/react";
import { UserSignoutView } from "./userSignoutView";

describe("test account group component: userSignoutView", () => {
    it("should render correctly '<UserSignoutView />' when content is loaded.", () => {
        const html = render(
            <BrowserRouter>
                <UserSignoutView
                    isLoading={false}
                    languageId="en"
                    caption="Sign out"
                    status="Signing out current user..., please wait."
                    buttonText={"Go back to main"}
                    isAnonymous={false}
                    className="mt-5"
                />
            </BrowserRouter>
        );

        expect(html).toMatchSnapshot();
    });
});
