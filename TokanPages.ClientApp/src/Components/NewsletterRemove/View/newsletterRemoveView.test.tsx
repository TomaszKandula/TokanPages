import "../../../setupTests";
import React from "react";
import { render } from "enzyme";
import { NewsletterRemoveView } from "./newsletterRemoveView";
import { ContentDto } from "../../../Api/Models";

describe("test component: newsletterRemoveView", () => {
    it("should render correctly '<NewsletterRemoveView />' when content is loaded.", () => {
        const content: ContentDto = {
            caption: "Cancel your subscribtion",
            text1: "We are sorry to see you go...",
            text2: "...but we understand there are circumstances",
            text3: "Please contact us should you have any questions",
            button: "Unsubscribe",
        };

        const html = render(
            <NewsletterRemoveView
                isLoading={false}
                buttonHandler={jest.fn()}
                buttonState={false}
                progress={false}
                contentPre={content}
                contentPost={content}
                isRemoved={false}
            />
        );

        expect(html).toMatchSnapshot();
    });
});
