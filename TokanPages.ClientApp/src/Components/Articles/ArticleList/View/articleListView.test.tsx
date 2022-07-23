import "../../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { IAuthor } from "../../../../Shared/Components/ContentRender/Models";
import { ITextItem } from "../../../../Shared/Components/ContentRender/Models";
import { IArticleItem } from "../../../../Shared/Components/ContentRender/Models";
import { ArticleListView } from "../View/articleListView";

describe("Test articles group component: ArticleListView.", () => 
{
    it("Renders correctly '<ArticleListView />' when content is loaded.", () => 
    {
        const author: IAuthor = 
        {
            aliasName: "Ester",
            avatarName: "default.jpg",
            firstName: "Ester",
            lastName: "Exposito",
            shortBio: "Happy developer",
            registered: "2020-01-05 14:15:30"
        };
        
        const text: ITextItem = 
        {
            id: "5b30be71-0e68-4be4-bd29-7f40fe130414",
            type: "html",
            value: "<p>One line text</p>",
            prop: "",
            text: ""
        };

        const articles: IArticleItem = 
        {  
            id: "8919332f-6654-4346-946f-5204cb7a3234",
            title: "Test article",
            description: "Some article",
            isPublished: false,
            likeCount: 0,
            userLikes: 0,
            readCount: 0,
            createdAt: "2020-09-21",
            updatedAt: "2020-12-05",
            author: author,
            text: [text],
        };

        const tree = shallow(<ArticleListView bind=
        {{
            isLoading: false,
            articles: [articles]
        }}/>);
        expect(tree).toMatchSnapshot();
    });
});
