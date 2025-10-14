import * as React from "react";
import { NavigationViewBaseProps } from "../../Types";
import { RenderAvatarIcon } from "../RenderAvatarIcon";
import { RenderSelectionIcon } from "../RenderSelectionIcon";
import { Image } from "../../../../../Shared/Components";
import { GET_IMAGES_URL } from "../../../../../Api";
import { LanguageItemDto } from "../../../../../Api/Models";
import { v4 as uuidv4 } from "uuid";

const baseStyle = "bulma-navbar-item bulma-has-dropdown mr-4";

const RenderSelection = (props: NavigationViewBaseProps): React.ReactElement => (
    <div className={`${baseStyle} ${props.isLanguageMenuOpen ? "bulma-is-active" : ""}`}>
        <a className="bulma-navbar-link is-transparent" onClick={props.languageMenuHandler}>
            <Image
                base={GET_IMAGES_URL}
                source={`${props.languageFlagDir}/${props.languageId}.${props.languageFlagType}`}
                title="Language flag"
                alt={`A flag (${props.languageId}) for current language selection`}
                className="bulma-image bulma-is-16x16 is-round-border"
            />
            <div>{props.languageId?.toUpperCase()}</div>
        </a>
        <div className="bulma-navbar-dropdown bulma-is-boxed bulma-is-right" onMouseLeave={props.languageMenuHandler}>
            {props.languages?.languages.map((item: LanguageItemDto, _index: number) => (
                <a className="bulma-navbar-item" key={uuidv4()} onClick={() => props.languagePickHandler(item.id)}>
                    <Image
                        base={GET_IMAGES_URL}
                        source={`${props.languageFlagDir}/${item.id}.${props.languageFlagType}`}
                        title="Language flag"
                        alt={`A flag (${item.name}) symbolizing available language`}
                        className="bulma-image bulma-is-16x16 is-round-border mr-4"
                    />
                    <div>{item.name}</div>
                    <RenderSelectionIcon selection={item.id} languageId={props.languageId} size={1.3} />
                </a>
            ))}
        </div>
    </div>
);

export const RenderLanguageSection = (props: NavigationViewBaseProps): React.ReactElement => (
    <>
        <RenderSelection {...props} />
        {props.isAnonymous ? null : <RenderAvatarIcon {...props} />}
    </>
);
