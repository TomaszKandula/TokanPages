import "../../../../setupTests";
import React from "react";
import { render } from "enzyme";
import { ArticleCardView } from "./articleCardView";

describe("test articles group component: ArticleCardView", () => {
    it("should render correctly '<ArticleCardView />' when content is loaded.", () => {
        const html = render(
            <ArticleCardView
                imageUrl="/images/card.jpg"
                title="Article title"
                description="Article short description"
                onClickEvent={jest.fn()}
                buttonText="READ"
                flagImage="eng.png"
            />
        );

        expect(html).toMatchSnapshot();
    });
});
