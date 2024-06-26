import "../../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { ArticleCardView } from "./articleCardView";

describe("test articles group component: ArticleCardView", () => {
    it("should render correctly '<ArticleCardView />' when content is loaded.", () => {
        const tree = shallow(
            <ArticleCardView
                imageUrl="/images/card.jpg"
                title="Article title"
                description="Article short description"
                onClickEvent={jest.fn()}
                buttonText="READ"
                flagImage="eng.png"
            />
        );

        expect(tree).toMatchSnapshot();
    });
});
