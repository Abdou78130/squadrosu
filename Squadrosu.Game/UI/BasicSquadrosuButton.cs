// Copyright (c) SquadroCorp
// This file is part of Squadrosu!.
// Squadrosu! is licensed under the GPL v3. See LICENSE.md for details.

using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Graphics.Audio;

namespace Squadrosu.Game.UI;

/// <summary>
/// Pretty button for the main menu. Has heavy samples and moves a bit to the left on hover.
/// </summary>
public class BasicSquadrosuButton : SquadrosuButton
{
    public BasicSquadrosuButton() : base()
    {
        BackgroundCornerRadius = 10f;
        HasShadow = false;
    }

    [BackgroundDependencyLoader]
    private void load(AudioManager audio)
    {
        SampleHover = new DrawableSample(audio.Samples.Get(@"default-hover"));
        SampleClick = new DrawableSample(audio.Samples.Get(@"default-select"));
    }
}
