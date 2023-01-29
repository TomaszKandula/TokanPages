import "../../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { ArticleCardView } from "./articleCardView";

describe("Test articles group component: ArticleCardView.", () => 
{
    it("Renders correctly '<ArticleCardView />' when content is loaded.", () => 
    {
        const tree = shallow(<ArticleCardView
            imageUrl="/images/card.jpg"
            title="Article title"
            description="Article short description"
            onClickEvent={jest.fn()}
            buttonText="READ"
        />);

        expect(tree).toMatchSnapshot();
    });
});
