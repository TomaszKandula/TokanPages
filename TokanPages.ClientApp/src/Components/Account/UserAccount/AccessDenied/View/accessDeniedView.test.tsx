import "../../../../../setupTests";
import React from "react";
import { BrowserRouter } from "react-router-dom";
import { render } from "@testing-library/react";
import { AccessDeniedView } from "./accessDeniedView";

describe("test account group component: accessDeniedView", () => {
    it("should render correctly '<AccessDeniedView />' when content is loaded.", () => {
        const html = render(
            <BrowserRouter>
                <AccessDeniedView
                    isLoading={false}
                    languageId="en"
                    accessDeniedCaption={"Access Denied"}
                    accessDeniedPrompt={[
                        "You do not have access to this page.",
                        "If you believe this is an error, please contact IT support.",
                    ]}
                    homeButtonText={"Go back"}
                />
            </BrowserRouter>
        );

        expect(html).toMatchSnapshot();
    });
});
