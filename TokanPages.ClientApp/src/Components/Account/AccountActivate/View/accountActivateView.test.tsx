import "../../../../setupTests";
import React from "react";
import { render } from "@testing-library/react";
import { AccountActivateView } from "./accountActivateView";

describe("test account group component: accountActivateView", () => {
    it("should render correctly '<AccountActivateView />' when content is loaded.", () => {
        const html = render(
            <AccountActivateView
                isLoading={false}
                caption={"Account Activation"}
                text1={"Your account has been successfully activated!"}
                text2={"You can now sign in."}
                progress={false}
            />
        );

        expect(html).toMatchSnapshot();
    });
});
