// Copyright (c) SquadroCorp
// This file is part of Squadrosu!.
// Squadrosu! is licensed under the GPL v3. See LICENSE.md for details.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK.Graphics;
using Squadrosu.Game.UI;

namespace Squadrosu.Game.Tests.Visual.UI;

public class TestSceneSquadrosuHueSelector : SquadrosuTestScene
{
    [BackgroundDependencyLoader]
    private void load()
    {
        Add(new Container
        {
            RelativeSizeAxes = Axes.Both,
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Colour = Color4.DarkSlateGray,
                },
                new Container
                {
                    Width = 500,
                    Height = 42,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Child = new SquadrosuHueSelector(),
                },
            },
        });
    }
}
