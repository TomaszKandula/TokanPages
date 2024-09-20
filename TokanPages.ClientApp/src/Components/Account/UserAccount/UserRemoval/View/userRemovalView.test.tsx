import "../../../../../setupTests";
import React from "react";
import { BrowserRouter } from "react-router-dom";
import { render } from "@testing-library/react";
import { UserRemovalView } from "./userRemovalView";

describe("test account group component: userRemovalView", () => {
    it("should render correctly '<UserRemovalView />' when content is loaded.", () => {
        const html = render(
            <BrowserRouter>
                <UserRemovalView
                    isLoading={false}
                    deleteButtonHandler={jest.fn()}
                    deleteAccountProgress={true}
                    sectionAccountRemoval={{
                        caption: "Account Removal",
                        warningText: "<p>Warning!</p><p>Once you remove your existing account, you will lose all your data...</p>",
                        deleteButtonText: "Delete",
                    }}
                />
            </BrowserRouter>
        );

        expect(html).toMatchSnapshot();
    });
});
