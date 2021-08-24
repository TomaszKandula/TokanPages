import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import ArticleDetailView from "../articleDetailView";

describe("Test articles group component: articleDetailView.", () => 
{
    it("Renders correctly '<ArticleDetailView />' when content is loaded.", () => 
    {
        const tree = shallow(<ArticleDetailView bind=
        {{
            backButtonHandler: jest.fn(),
            articleReadCount: 0,
            openPopoverHandler: jest.fn(),
            closePopoverHandler: jest.fn(),
            renderSmallAvatar: <div>renderSmallAvatar</div>,
            renderLargeAvatar: <div>renderLargeAvatar</div>,
            authorAliasName: "Ester",
            popoverOpen: false,
            popoverElement: null,
            authorFirstName: "Ester",
            authorLastName: "Exposito",
            authorRegistered: "",
            articleReadTime: "4 min.",
            articleCreatedAt: "2020-07-21",
            articleUpdatedAt: "2020-09-12",
            articleContent: <div>articleContent</div>,
            renderLikesLeft: "25",
            thumbsHandler: jest.fn(),
            totalLikes: 50,
            renderAuthorName: "Ester Exposito",
            authorShortBio: "Happy developer",
        }}/>);
        expect(tree).toMatchSnapshot();
    });
});
