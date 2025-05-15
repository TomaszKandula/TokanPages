import * as React from "react";
import { BaseProperties, Properties } from "../../Abstractions";
import { RenderAvatarIcon } from "../RenderAvatarIcon";
import { RenderLanguageIcon } from "../RenderLanguageIcon";
import { RenderImage } from "../../../../../Shared/Components";
import { GET_FLAG_URL } from "../../../../../Api";
import { LanguageItemDto } from "../../../../../Api/Models";
import { v4 as uuidv4 } from "uuid";

export const RenderLanguageSection = (props: BaseProperties): React.ReactElement => {
    return (
        <>
            <div className="navigation-languages-box">
                <RenderSelection {...props} />
            </div>
            {props.isAnonymous ? null : <RenderAvatarIcon {...props} />}
        </>
    );
};

const RenderSelection = (props: Properties): React.ReactElement => {
    const toUpper = (value?: any): string | undefined => {
        if (value !== undefined) {
            return (value as string).toUpperCase();
        }

        return undefined;
    };

    return (
        <div className="bulma-navbar-item bulma-has-dropdown bulma-is-hoverable">
            <a className="bulma-navbar-link">
                <RenderImage
                    base={GET_FLAG_URL}
                    source={`${props.languageId}.png`}
                    title="Language flag"
                    alt={`A flag (${props.languageId}) for current language selection`}
                    className="navigation-flag-image"
                />
                <div>{toUpper(props.languageId)}</div>
            </a>
            <div className="bulma-navbar-dropdown bulma-is-boxed bulma-is-right">
                {props.languages?.languages.map((item: LanguageItemDto, _index: number) => (
                    <a className="bulma-navbar-item" key={uuidv4()} onClick={() => props.languageHandler(item.id)}>
                        <RenderImage
                            base={GET_FLAG_URL}
                            source={`${item.id}.png`}
                            title="Language flag"
                            alt={`A flag (${item.name}) symbolizing available language`}
                            className="navigation-flag-image"
                        />
                        <div>{item.name}</div>
                        <RenderLanguageIcon selection={item.id} languageId={props.languageId} />
                    </a>
                ))}
            </div>
        </div>
    );
};
