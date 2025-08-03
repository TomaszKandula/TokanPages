import * as React from "react";
import { BaseProperties } from "../../Abstractions";
import { RenderAvatarIcon } from "../RenderAvatarIcon";
import { RenderLanguageIcon } from "../RenderLanguageIcon";
import { CustomImage } from "../../../../../Shared/Components";
import { GET_FLAG_URL } from "../../../../../Api";
import { LanguageItemDto } from "../../../../../Api/Models";
import { v4 as uuidv4 } from "uuid";

const baseStyle = "bulma-navbar-item bulma-has-dropdown mr-4";

const RenderSelection = (props: BaseProperties): React.ReactElement => (
    <div className={`${baseStyle} ${props.isLanguageMenuOpen ? "bulma-is-active" : ""}`}>
        <a className="bulma-navbar-link" onClick={props.languageMenuHandler}>
            <CustomImage
                base={GET_FLAG_URL}
                source={`${props.languageId}.png`}
                title="Language flag"
                alt={`A flag (${props.languageId}) for current language selection`}
                className="bulma-image bulma-is-16x16"
            />
            <div>{props.languageId?.toUpperCase()}</div>
        </a>
        <div className="bulma-navbar-dropdown bulma-is-boxed bulma-is-right" onMouseLeave={props.languageMenuHandler}>
            {props.languages?.languages.map((item: LanguageItemDto, _index: number) => (
                <a className="bulma-navbar-item" key={uuidv4()} onClick={() => props.languagePickHandler(item.id)}>
                    <CustomImage
                        base={GET_FLAG_URL}
                        source={`${item.id}.png`}
                        title="Language flag"
                        alt={`A flag (${item.name}) symbolizing available language`}
                        className="bulma-image bulma-is-16x16 mr-4"
                    />
                    <div>{item.name}</div>
                    <RenderLanguageIcon selection={item.id} languageId={props.languageId} />
                </a>
            ))}
        </div>
    </div>
);

export const RenderLanguageSection = (props: BaseProperties): React.ReactElement => (
    <>
        <RenderSelection {...props} />
        {props.isAnonymous ? null : <RenderAvatarIcon {...props} />}
    </>
);
