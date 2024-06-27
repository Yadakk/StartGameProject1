using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private bool _playOnStart;
    [SerializeField] private List<AudioClip> _clips;
    private AudioSource _source;
    public AudioSource Source { get => _source; }
    private Coroutine _musicRoutine;

    private void Start()
    {
        if (_source == null) _source = GetComponent<AudioSource>();
        if (_playOnStart) Play();
    }

    private IEnumerator MusicRoutine()
    {
        while (true)
        {
            for (int i = 0; i < _clips.Count; i++)
            {
                var clip = _clips[i];
                _source.clip = clip;
                _source.Play();
                yield return new WaitForSecondsRealtime(clip.length);
            }

            _clips = _clips.ShuffleWithoutRepetition();
        }
    }

    public void Stop()
    {
        if (_musicRoutine != null)
        {
            StopCoroutine(_musicRoutine);
            _source.Stop();
            _musicRoutine = null;
        }
    }

    public void Play()
    {
        if (_source == null) _source = GetComponent<AudioSource>();

        if (_source.clip != null)
            _clips = _clips.ShuffleWithoutRepetition(_source.clip);
        else
            _clips = _clips.ShuffleWithoutRepetition();

        _musicRoutine = StartCoroutine(MusicRoutine());
    }
}