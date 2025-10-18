import "../../../../setupTests";
import React from "react";
import { BrowserRouter as Router } from "react-router-dom";
import { render } from "@testing-library/react";
import { Author } from "../../../../Shared/Components/RenderContent/Models";
import { TextItem } from "../../../../Shared/Components/RenderContent/Models";
import { ArticleItem } from "../../../../Shared/Components/RenderContent/Models";
import { ArticleListView } from "../View/articleListView";

describe("test articles group component: ArticleListView", () => {
    it("should render correctly '<ArticleListView />' when content is loaded.", () => {
        const author: Author = {
            userId: "52a520f3-9f60-4c76-b0f5-926dc5168a6e",
            aliasName: "Ester",
            avatarName: "default.jpg",
            firstName: "Ester",
            lastName: "Exposito",
            shortBio: "Happy developer",
            registered: "2020-01-05 14:15:30",
        };

        const text: TextItem = {
            id: "5b30be71-0e68-4be4-bd29-7f40fe130414",
            type: "html",
            value: "<p>One line text</p>",
            prop: "",
            text: "",
        };

        const articles: ArticleItem = {
            id: "8919332f-6654-4346-946f-5204cb7a3234",
            title: "Test article",
            description: "Some article",
            isPublished: false,
            totalLikes: 0,
            userLikes: 0,
            readCount: 0,
            createdAt: "2020-09-21",
            updatedAt: "2020-12-05",
            languageIso: "spa",
            author: author,
            text: [text],
        };

        const html = render(
            <Router>
                <ArticleListView
                    isLoading={false}
                    isMobile={false}
                    isContentLoading={false}
                    isOrderByAscending={false}
                    isSnapshot={false}
                    onSortClick={jest.fn()}
                    pageData={{
                        pageNumber: 1,
                        pageSize: 5,
                        totalSize: 10,
                        paginationNumber: 5,
                        onClick: jest.fn(),
                    }}
                    selectedCategory=""
                    categories={[]}
                    articles={[articles]}
                    title=""
                    text={[""]}
                    placeholder=""
                    searchInputForm={{ value: "" }}
                    searchEmptyText1="Not Found"
                    searchEmptyText2="Try again..."
                    onChange={jest.fn()}
                    onKeyUp={jest.fn()}
                    onCategoryChange={jest.fn()}
                    buttonSearch={{
                        isSearchDisabled: false,
                        label: "",
                        onClick: jest.fn(),
                    }}
                    buttonClear={{
                        isClearDisabled: false,
                        label: "",
                        onClick: jest.fn(),
                    }}
                />
            </Router>
        );

        expect(html).toMatchSnapshot();
    });
});
