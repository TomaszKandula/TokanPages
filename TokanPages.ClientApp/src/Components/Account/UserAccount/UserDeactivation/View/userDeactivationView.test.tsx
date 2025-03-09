import "../../../../../setupTests";
import React from "react";
import { BrowserRouter } from "react-router-dom";
import { render } from "@testing-library/react";
import { UserDeactivationView } from "./userDeactivationView";

describe("test account group component: userDeactivationView", () => {
    it("should render correctly '<UserDeactivationView />' when content is loaded.", () => {
        const html = render(
            <BrowserRouter>
                <UserDeactivationView
                    isLoading={false}
                    progress={false}
                    buttonHandler={jest.fn()}
                    section={{
                        caption: "Account Deactivation",
                        warningText: ["Warning!","You can deactivate the account..."],
                        deactivateButtonText: "Dactivate",
                    }}
                />
            </BrowserRouter>
        );

        expect(html).toMatchSnapshot();
    });
});
