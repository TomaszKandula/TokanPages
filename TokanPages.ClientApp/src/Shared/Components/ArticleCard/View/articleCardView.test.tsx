import "../../../../setupTests";
import React from "react";
import { render } from "@testing-library/react";
import { ArticleCardView } from "./articleCardView";

describe("test articles group component: ArticleCardView", () => {
    it("should render correctly '<ArticleCardView />' when content is loaded.", () => {
        const html = render(
            <ArticleCardView
                isLoading={false}
                imageUrl="/images/card.jpg"
                title="Article title"
                description="Article short description"
                onClickEvent={jest.fn()}
                buttonText="READ"
                flagImage="eng.png"
                canAnimate={false}
                canDisplayDate={true}
                published="2020-01-01"
                readCount={"1.000"}
                totalLikes="1.951"
            />
        );

        expect(html).toMatchSnapshot();
    });
});
