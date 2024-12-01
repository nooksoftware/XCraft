sampler2D TextureSampler : register(s0);

// Parameters
float2 motionDirection; // Direction of motion (normalized)
float blurStrength;     // Amount of blur (e.g., number of samples)

// Pixel Shader
float4 MainPS(float2 texCoord : TEXCOORD) : COLOR
{
    float4 finalColor = float4(0, 0, 0, 0);
    float totalWeight = 0.0;

    // Accumulate samples
    for (int i = 0; i < blurStrength; i++)
    {
        float weight = (1.0 - i / blurStrength);
        float2 sampleOffset = motionDirection * (i / blurStrength) * 0.02; // Adjust scale
        finalColor += tex2D(TextureSampler, texCoord + sampleOffset) * weight;
        totalWeight += weight;
    }

    return finalColor / totalWeight; // Normalize color
}

technique MotionBlur
{
    pass P0
    {
        PixelShader = compile ps_4_0 MainPS();
    }
}