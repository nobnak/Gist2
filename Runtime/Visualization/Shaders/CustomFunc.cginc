#ifndef __CUSTOM_FUNC_CGINC__
#define __CUSTOM_FUNC_CGINC__


float4 OutputMixer(
	in float4 C0,
	in float4 C1,
	in float4 C2,
	in float4 C3,
	in float4 M) {

	float wsum = dot(1, M);
	float4 w = (wsum > 1e-3f) ? M / wsum : 0;

	return w.x * C0 + w.y * C1 + w.z * C2 + w.w * C3;
}

void Mixer_float(
	in float4 C0,
	in float4 C1,
	in float4 C2,
	in float4 C3,
	in float4 M,
	out float4 OUT) {

	OUT = OutputMixer(C0, C1, C2, C3, M);
}


#endif