import "../../../../setupTests";
import React from "react";
import { render } from "@testing-library/react";
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
                canAnimate={false}
                readCount={1000}
            />
        );

        expect(html).toMatchSnapshot();
    });
});
