import "../../../../setupTests";
import React from "react";
import { render } from "@testing-library/react";
import { ArticleContentDto } from "../../../../Api/Models";
import { ArticleDetailView } from "./articleDetailView";

describe("test articles group component: ArticleDetailView", () => {
    it("should render correctly '<ArticleDetailView />' when content is loaded.", () => {
        const testContent: ArticleContentDto = {
            language: "eng",
            button: "Read now",
            textReadCount: "Read count:",
            textFirstName: "First name:",
            textSurname: "Last name:",
            textRegistered: "Registered at:",
            textLanguage: "Article language:",
            textReadTime: "Read time:",
            textPublished: "Published at:",
            textUpdated: "Updated at:",
            textWritten: "Written by",
            textAbout: "About the author:",
        };

        const html = render(
            <ArticleDetailView
                backButtonHandler={jest.fn()}
                articleReadCount={"1.000.000"}
                openPopoverHandler={jest.fn()}
                closePopoverHandler={jest.fn()}
                renderSmallAvatar={<div>renderSmallAvatar</div>}
                renderLargeAvatar={<div>renderLargeAvatar</div>}
                authorAliasName="Ester"
                popoverOpen={false}
                popoverElement={null}
                authorFirstName="Ester"
                authorLastName="Exposito"
                authorRegistered=""
                articleReadTime="4"
                articleCreatedAt="2020-01-10T12:15:15"
                articleUpdatedAt="2020-01-10T12:15:15"
                articleContent={<div>articleContent</div>}
                renderLikesLeft="25"
                thumbsHandler={jest.fn()}
                totalLikes={"1.150"}
                renderAuthorName="Ester Exposito"
                authorShortBio="Happy developer"
                flagImage="eng.png"
                content={testContent}
            />
        );

        expect(html).toMatchSnapshot();
    });
});
